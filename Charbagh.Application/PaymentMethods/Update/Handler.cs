using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Update;

public sealed class UpdatePaymentMethodHandler
    : IRequestHandler<
        UpdatePaymentMethodCommand,
        PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdatePaymentMethodHandler(
        IPaymentMethodRepository paymentMethodRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> Handle(
        UpdatePaymentMethodCommand request,
        CancellationToken cancellationToken)
    {
        var paymentMethod =
            await _paymentMethodRepository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (paymentMethod is null)
            throw new KeyNotFoundException(
                "Payment method not found.");

        var normalizedName = request.Name.Trim();

        if (!string.Equals(
                paymentMethod.Name,
                normalizedName,
                StringComparison.OrdinalIgnoreCase))
        {
            var exists =
                await _paymentMethodRepository.ExistsByNameAsync(
                    normalizedName,
                    cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A payment method with this name already exists.");
        }

        paymentMethod.Update(
            request.Name,
            request.Description,
            request.DisplayOrder);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return paymentMethod.ToDto();
    }
}