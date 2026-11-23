
namespace Charbagh.Application.InvoiceItems.Common
{
    public sealed record InvoiceItemDto(
        Guid Id,
        Guid InvoiceId,
        Guid? ServicePriceId,
        InvoiceItemType Type,
        string Description,
        decimal UnitPrice,
        int Quantity
        );
}
