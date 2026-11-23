using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.GetAll;

public sealed class GetAllPaymentMethodsHandler
    : IRequestHandler<
        GetAllPaymentMethodsQuery,
        IReadOnlyList<PaymentMethodDto>>
{
    private readonly IPaymentMethodRepository _paymentMethodRepository;

    public GetAllPaymentMethodsHandler(
        IPaymentMethodRepository paymentMethodRepository)
    {
        _paymentMethodRepository = paymentMethodRepository;
    }

    public async Task<IReadOnlyList<PaymentMethodDto>> Handle(
        GetAllPaymentMethodsQuery request,
        CancellationToken cancellationToken)
    {
        var paymentMethods =
            await _paymentMethodRepository.GetAllAsync(
                cancellationToken);

        return paymentMethods
            .Select(x => x.ToDto())
            .ToList();
    }
}