using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Discounts.Common;

public static class DiscountMapper
{
    public static DiscountDto ToDto(
        this Discount discount)
    {
        return new DiscountDto(
            discount.Id,
            discount.InvoiceId,
            discount.DiscountTypeId,
            discount.ValueType,
            discount.Value,
            discount.Amount,
            discount.Description);
    }
}