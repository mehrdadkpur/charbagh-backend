
namespace Charbagh.Application.Teachers.Common
{
    public sealed record TeacherDto(
        Guid Id,
        Guid UserId,
        string FirstName,
        string LastName,
        string? Email,
        string? ProfileImageUrl,
        string PersonnelCode, 
        DateOnly EmploymentDate, 
        UserEducation? Education, 
        int? ExperienceYears, 
        string? Biography , 
        TeacherStatus Status
        );
}
