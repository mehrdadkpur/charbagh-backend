using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.Update
{
    public sealed record UpdateInvoiceItemCommand(
        Guid Id,
        Guid InvoiceId,
        Guid? ServicePriceId,
        InvoiceItemType Type,
        string Description,
        decimal UnitPrice,
        int Quantity
        ) :IRequest<InvoiceItemDto>;

}
