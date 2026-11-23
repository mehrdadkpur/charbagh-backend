
namespace Charbagh.Application.EducationServices.Common
{
    public sealed record EducationServiceDto(
       Guid Id,
       string ServiceName,
       string? Description,
       bool IsActive
        );
}
