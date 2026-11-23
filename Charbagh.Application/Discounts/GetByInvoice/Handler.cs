using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.GetByInvoice;

public sealed class GetDiscountByInvoiceHandler
    : IRequestHandler<GetDiscountByInvoiceQuery, DiscountDto?>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountByInvoiceHandler(
        IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiscountDto?> Handle(
        GetDiscountByInvoiceQuery request,
        CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByInvoiceIdAsync(
            request.InvoiceId,
            cancellationToken);

        return discount?.ToDto();
    }
}