using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.GetById;

public sealed class GetPaymentByIdHandler : IRequestHandler<GetPaymentByIdQuery, PaymentDto>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentByIdHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<PaymentDto> Handle(
        GetPaymentByIdQuery request,
        CancellationToken cancellationToken)
    {
        var payment = await _paymentRepository
            .GetByIdAsync(
                request.Id,
                cancellationToken);

        if (payment is null)
            throw new KeyNotFoundException(
                "Payment was not found.");

        return payment.ToDto();
    }
}