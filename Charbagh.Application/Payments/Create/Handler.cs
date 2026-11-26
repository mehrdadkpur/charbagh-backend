using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Payments.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.Payments.Create;

public sealed class CreatePaymentHandler : IRequestHandler<CreatePaymentCommand, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentHandler(
        IPaymentRepository paymentRepository,
        IInvoiceRepository invoiceRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _invoiceRepository = invoiceRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentDto> Handle(
        CreatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(
                request.InvoiceId,
                cancellationToken);

        if (invoice is null)
            throw new KeyNotFoundException(
                "Invoice was not found.");

        if (invoice.IsCancelled)
        {
            throw new InvalidOperationException(
                "Cancelled invoices cannot receive payments.");
        }

        if (invoice.Status == InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Draft invoices cannot receive payments.");
        }

        if (invoice.IsPaid)
        {
            throw new InvalidOperationException(
                "Paid invoices cannot receive payments.");
        }

        var paymentMethod = await _paymentMethodRepository
            .GetByIdAsync(
                request.PaymentMethodId,
                cancellationToken);

        if (paymentMethod is null)
            throw new KeyNotFoundException(
                "Payment method was not found.");

        if (!paymentMethod.IsActive)
            throw new InvalidOperationException(
                "Selected payment method is inactive.");

        if (request.PaidByUserId.HasValue)
        {
            var user = await _userRepository.GetByIdAsync(
                request.PaidByUserId.Value,
                cancellationToken);

            if (user is null)
                throw new KeyNotFoundException(
                    "Paid by user was not found.");
        }

        if (request.Amount <= 0)
            throw new ArgumentException(
                "Payment amount must be greater than zero.",
                nameof(request.Amount));

        if (request.Amount > invoice.RemainingAmount)
            throw new InvalidOperationException(
                "Payment amount cannot be greater than invoice remaining amount.");

        var payment = Payment.Create(
            request.InvoiceId,
            request.PaymentMethodId,
            request.Amount,
            request.PaymentDate,
            request.PaidByUserId,
            request.ReferenceNumber,
            request.Note);

        await _paymentRepository.AddAsync(
            payment,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return payment.ToDto();
    }
}