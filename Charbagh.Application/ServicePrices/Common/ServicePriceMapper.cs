
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.ServicePrices.Common
{
    public static class ServicePriceMapper
    {
        public static ServicePriceDto ToDto(this ServicePrice servicePrice)
        {
            return new ServicePriceDto(
                servicePrice.Id,
                servicePrice.EducationServiceId,
                servicePrice.Price,
                servicePrice.EffectiveFrom,
                servicePrice.EffectiveTo,
                servicePrice.IsActive
                );
        }
    }
}
