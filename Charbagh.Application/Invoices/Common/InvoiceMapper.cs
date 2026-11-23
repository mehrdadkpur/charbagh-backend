using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Invoices.Common
{
    public static class InvoiceMapper
    {
        public static InvoiceDto ToDto(this Invoice invoice)
        {
            return new InvoiceDto(invoice.Id, invoice.StudentId,invoice.TotalAmount, invoice.PaidAmount, invoice.RemainingAmount, invoice.Status, invoice.Description);
        }
    }
}
