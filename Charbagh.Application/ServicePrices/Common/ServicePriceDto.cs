
namespace Charbagh.Application.ServicePrices.Common
{
    public sealed record ServicePriceDto(
        Guid Id,
        Guid EducationServiceId,
        decimal Price,
        DateOnly EffectiveFrom,
        DateOnly? EffectiveTo,
        bool IsActive
        );
}
