using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Deactivate;

public sealed class DeactivatePaymentMethodHandler
    : IRequestHandler<
        DeactivatePaymentMethodCommand,
        PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivatePaymentMethodHandler(
        IPaymentMethodRepository paymentMethodRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> Handle(
        DeactivatePaymentMethodCommand request,
        CancellationToken cancellationToken)
    {
        var paymentMethod =
            await _paymentMethodRepository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (paymentMethod is null)
            throw new KeyNotFoundException(
                "Payment method not found.");

        paymentMethod.Deactivate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return paymentMethod.ToDto();
    }
}