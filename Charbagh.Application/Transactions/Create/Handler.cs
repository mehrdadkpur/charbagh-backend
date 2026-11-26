using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.Transactions.Create;

public sealed class CreateTransactionHandler
    : IRequestHandler<CreateTransactionCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IPaymentRepository _paymentRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateTransactionHandler(
        ITransactionRepository transactionRepository,
        IPaymentRepository paymentRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _paymentRepository = paymentRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto> Handle(
        CreateTransactionCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository.GetByIdAsync(
            request.PaymentId,
            cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException(
                "Payment was not found.");

        var user = await _userRepository.GetByIdAsync(
            request.CreatedByUserId,
            cancellationToken);

        if (user is null)
            throw new KeyNotFoundException(
                "Created by user was not found.");

        if (request.Amount <= 0)
            throw new ArgumentException(
                "Transaction amount must be greater than zero.",
                nameof(request.Amount));

        if (!Enum.IsDefined(typeof(TransactionType), request.Type))
            throw new ArgumentException(
                "Transaction type is invalid.",
                nameof(request.Type));

        var relatedTransactions =
            await _transactionRepository.GetByPaymentIdAsync(
                payment.Id,
                cancellationToken);

        if (request.Type == TransactionType.PaymentReceived)
        {
            var hasActiveReceipt = relatedTransactions.Any(x =>
                x.Type == TransactionType.PaymentReceived &&
                (x.Status == TransactionStatus.Pending ||
                 x.Status == TransactionStatus.Successful));

            if (hasActiveReceipt)
            {
                throw new InvalidOperationException(
                    "A pending or successful receipt transaction already exists for this payment.");
            }

            if (request.Amount != payment.Amount)
            {
                throw new InvalidOperationException(
                    "Receipt transaction amount must equal the payment amount.");
            }

            if (request.Amount > payment.Invoice.RemainingAmount)
            {
                throw new InvalidOperationException(
                    "Receipt transaction amount exceeds the invoice remaining amount.");
            }
        }

        if (request.Type == TransactionType.Refund)
        {
            var hasReceiptHistory = relatedTransactions.Any(
                x => x.Type == TransactionType.PaymentReceived);

            if (hasReceiptHistory &&
                !relatedTransactions.Any(x =>
                    x.Type == TransactionType.PaymentReceived &&
                    x.Status == TransactionStatus.Successful))
            {
                throw new InvalidOperationException(
                    "A payment cannot be refunded before its receipt transaction succeeds.");
            }

            if (request.Amount > payment.Invoice.PaidAmount)
            {
                throw new InvalidOperationException(
                    "Refund amount exceeds the invoice paid amount.");
            }

            var alreadyReserved = relatedTransactions
                .Where(x => x.Type == TransactionType.Refund &&
                            (x.Status == TransactionStatus.Pending ||
                             x.Status == TransactionStatus.Successful))
                .Sum(x => x.Amount);

            var refundableAmount = payment.Amount - alreadyReserved;

            if (request.Amount > refundableAmount)
            {
                throw new InvalidOperationException(
                    "Refund amount exceeds the remaining refundable payment amount.");
            }
        }

        var transaction = Transaction.Create(
            request.PaymentId,
            request.CreatedByUserId,
            request.Type,
            request.Amount,
            request.TransactionDate,
            request.ReferenceNumber,
            request.Description);

        await _transactionRepository.AddAsync(
            transaction,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return transaction.ToDto();
    }
}