using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.GetById;

public sealed class GetPaymentMethodByIdHandler: IRequestHandler< GetPaymentMethodByIdQuery,PaymentMethodDto>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public GetPaymentMethodByIdHandler(
        IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<PaymentMethodDto> Handle(
        GetPaymentMethodByIdQuery request,
        CancellationToken cancellationToken)
    {
        var paymentMethod =
            await _paymentMethodRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

        if (paymentMethod is null)
            throw new KeyNotFoundException(
                "Payment method not found.");

        return paymentMethod.ToDto();
    }
}