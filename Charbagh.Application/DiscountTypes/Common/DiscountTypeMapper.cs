using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.DiscountTypes.Common;

public static class DiscountMapper
{
    public static DiscountTypeDto ToDto(
        this DiscountType discountType)
    {
        return new DiscountTypeDto(
            discountType.Id,
            discountType.Name,
            discountType.Description,
            discountType.IsActive,
            discountType.DisplayOrder);
    }
}