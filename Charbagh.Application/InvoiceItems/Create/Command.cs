
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.Create
{
    public sealed record CreateInvoiceItemCommand(
        Guid InvoiceId,
        Guid? ServicePriceId,
        InvoiceItemType Type,
        string Description,
        decimal UnitPrice,
        int Quantity
        ) :IRequest<InvoiceItemDto>;

}
