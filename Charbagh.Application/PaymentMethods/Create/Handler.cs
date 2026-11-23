using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Create;

public sealed class CreatePaymentMethodHandler
    : IRequestHandler<
        CreatePaymentMethodCommand,
        PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreatePaymentMethodHandler(
        IPaymentMethodRepository paymentMethodRepository,
        IUnitOfWork unitOfWork)
    {
        _paymentMethodRepository = paymentMethodRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<PaymentMethodDto> Handle(
        CreatePaymentMethodCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _paymentMethodRepository.ExistsByNameAsync(
            request.Name.Trim(),
            cancellationToken);

        if (exists)
            throw new InvalidOperationException(
                "A payment method with this name already exists.");

        var paymentMethod = PaymentMethod.Create(
            request.Name,
            request.Description,
            request.DisplayOrder);

        await _paymentMethodRepository.AddAsync(
            paymentMethod,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return paymentMethod.ToDto();
    }
}