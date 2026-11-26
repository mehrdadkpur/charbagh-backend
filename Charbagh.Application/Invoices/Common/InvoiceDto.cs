
namespace Charbagh.Application.Invoices.Common
{
    public sealed record InvoiceDto(
        Guid Id,
        Guid StudentId,
        DateTime InvoiceDate,
        decimal Subtotal,
        decimal DiscountAmount,
        decimal TotalAmount,
        decimal PaidAmount,
        decimal RemainingAmount,
        InvoiceStatus Status,
        string? Description
        );
}
