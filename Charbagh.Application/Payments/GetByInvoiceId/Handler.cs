using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Payments.Common;
using Charbagh.Application.Payments.GetByInvoiceId;
using MediatR;

namespace Charbagh.Application.Features.Finance.Payments.GetByInvoice;

public sealed class GetPaymentsByInvoiceIdHandler
    : IRequestHandler<
        GetPaymentsByInvoiceQuery,
        IReadOnlyList<PaymentDto>>
{
    private readonly IPaymentRepository _paymentRepository;

    public GetPaymentsByInvoiceIdHandler(
        IPaymentRepository paymentRepository)
    {
        _paymentRepository = paymentRepository;
    }

    public async Task<IReadOnlyList<PaymentDto>> Handle(
        GetPaymentsByInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var payments = await _paymentRepository
            .GetByInvoiceIdAsync(
                request.InvoiceId,
                cancellationToken);

        return payments
            .Select(x => x.ToDto())
            .ToList();
    }
}