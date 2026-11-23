
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.Invoices.AddInvoiceItem
{
    public sealed record AddInvoiceItemCommand(
        Guid InvoiceId,
        Guid? ServicePriceId,
        InvoiceItemType Type,
        string Description,
         decimal UnitPrice,
        int Quantity
        ): IRequest<InvoiceItemDto>;
}
