using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Activate;

public sealed class ActivatePaymentMethodHandler
    : IRequestHandler<
        ActivatePaymentMethodCommand,
        PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivatePaymentMethodHandler(
        IPaymentMethodRepository paymentMethodRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> Handle(
        ActivatePaymentMethodCommand request,
        CancellationToken cancellationToken)
    {
        var paymentMethod =
            await _paymentMethodRepository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (paymentMethod is null)
            throw new KeyNotFoundException(
                "Payment method not found.");

        paymentMethod.Activate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return paymentMethod.ToDto();
    }
}