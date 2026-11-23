using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.InvoiceItems.Common
{
    public static class InvoiceItemMapper
    {
        public static InvoiceItemDto ToDto(this InvoiceItem invoiceItem)
        {
            return new InvoiceItemDto(
                invoiceItem.Id,
                invoiceItem.InvoiceId,
                invoiceItem.ServicePriceId,
                invoiceItem.Type,
                invoiceItem.Description,
                invoiceItem.UnitPrice,
                invoiceItem.Quantity
                );
        }
    }
}
