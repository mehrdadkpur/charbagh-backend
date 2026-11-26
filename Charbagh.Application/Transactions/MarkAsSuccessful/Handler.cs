using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.Transactions.MarkAsSuccessful;

public sealed class MarkTransactionSuccessfulHandler
    : IRequestHandler<MarkTransactionSuccessfulCommand, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkTransactionSuccessfulHandler(
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto> Handle(
        MarkTransactionSuccessfulCommand request,
        CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (transaction is null)
            throw new KeyNotFoundException("Transaction was not found.");

        if (transaction.Status == TransactionStatus.Successful)
            return transaction.ToDto();

        if (transaction.Type == TransactionType.PaymentReceived)
        {
            var relatedTransactions =
                await _transactionRepository.GetByPaymentIdAsync(
                    transaction.PaymentId,
                    cancellationToken);

            var anotherActiveReceipt = relatedTransactions.Any(x =>
                x.Id != transaction.Id &&
                x.Type == TransactionType.PaymentReceived &&
                (x.Status == TransactionStatus.Pending ||
                 x.Status == TransactionStatus.Successful));

            if (anotherActiveReceipt)
            {
                throw new InvalidOperationException(
                    "Another pending or successful receipt transaction already exists for this payment.");
            }

            transaction.Payment.Invoice.AddPayment(transaction.Amount);
        }

        if (transaction.Type == TransactionType.Refund)
        {
            var payment = transaction.Payment;
            var invoice = payment.Invoice;

            var relatedTransactions =
                await _transactionRepository.GetByPaymentIdAsync(
                    payment.Id,
                    cancellationToken);

            var reservedRefundAmount = relatedTransactions
                .Where(x => x.Id != transaction.Id &&
                            x.Type == TransactionType.Refund &&
                            (x.Status == TransactionStatus.Pending ||
                             x.Status == TransactionStatus.Successful))
                .Sum(x => x.Amount);

            var refundableAmount = payment.Amount - reservedRefundAmount;

            if (transaction.Amount > refundableAmount)
            {
                throw new InvalidOperationException(
                    "Refund amount exceeds the remaining refundable payment amount.");
            }

            invoice.RefundPayment(transaction.Amount);
        }

        transaction.MarkAsSuccessful();

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return transaction.ToDto();
    }
}
