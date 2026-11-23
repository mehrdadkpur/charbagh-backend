
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Teachers.Common
{
    public static class TeacherMapper
    {
        public static TeacherDto ToDto(this Teacher teacher)
        {
            ArgumentNullException.ThrowIfNull(teacher);

            var user = teacher.User
                ?? throw new InvalidOperationException(
                    $"User navigation property is null for Teacher: {teacher.Id}");

            return new TeacherDto(
                teacher.Id,
                teacher.UserId,
                user.FirstName,
                user.LastName,
                user.Email,
                user.ProfileImageUrl,
                teacher.PersonnelCode,
                teacher.EmploymentDate,
                teacher.Education,
                teacher.ExperienceYears,
                teacher.Biography,
                teacher.Status
            );
        }
    }
}
