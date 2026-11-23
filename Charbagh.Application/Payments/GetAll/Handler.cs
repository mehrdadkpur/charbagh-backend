using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.GetAll;

public sealed class GetAllPaymentsHandler: IRequestHandler<GetAllPaymentsQuery, IReadOnlyList<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetAllPaymentsHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IReadOnlyList<PaymentDto>> Handle( GetAllPaymentsQuery request, CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository.GetAllAsync(cancellationToken);

        return payments.Select(x => x.ToDto()).ToList();
    }
}