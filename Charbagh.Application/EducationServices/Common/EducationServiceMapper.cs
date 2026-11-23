using Charbagh.Domain.Entities.Settings;

namespace Charbagh.Application.EducationServices.Common
{
    public static class EducationServiceMapper
    {
        public static EducationServiceDto ToDto(this EducationService educationService)
        {
            return new EducationServiceDto(
                educationService.Id,
                educationService.ServiceName,
                educationService.Description,
                educationService.IsActive
                );
        }
    }
}
