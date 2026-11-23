using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.Update;

public sealed class UpdatePaymentHandler
    : IRequestHandler<UpdatePaymentCommand, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentHandler(
        IPaymentRepository paymentRepository,
        IPaymentMethodRepository paymentMethodRepository,
        IUserRepository userRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentRepository = paymentRepository;
        _paymentMethodRepository = paymentMethodRepository;
        _userRepository = userRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentDto> Handle(
        UpdatePaymentCommand request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository
            .GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException(
                "Payment was not found.");

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

        payment.Update(
            request.PaymentMethodId,
            request.Amount,
            request.PaymentDate,
            request.PaidByUserId,
            request.ReferenceNumber,
            request.Note);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return payment.ToDto();
    }
}