using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.PaymentMethods.Common;

public static class PaymentMethodMapper
{
    public static PaymentMethodDto ToDto(
        this PaymentMethod paymentMethod)
    {
        return new PaymentMethodDto(
            paymentMethod.Id,
            paymentMethod.Name,
            paymentMethod.Description,
            paymentMethod.IsActive,
            paymentMethod.DisplayOrder);
    }
}