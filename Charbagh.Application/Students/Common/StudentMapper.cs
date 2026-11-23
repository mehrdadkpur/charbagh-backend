
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Students.Common
{
    public static class StudentMapper
    {
        public static StudentDto ToDto(this Student student)
        {
            ArgumentNullException.ThrowIfNull(student);

            var user = student.User
                ?? throw new InvalidOperationException(
                    $"User navigation property is null for Student: {student.Id}");
            return new StudentDto(
                student.Id,
                student.UserId,
                user.FirstName,
                user.LastName,
                user.NationalIdentificationNumber,
                user.BirthDate,
                user.Email,
                user.ProfileImageUrl,
                student.StudentCode,
                student.RegistrationDate,
                student.Status
                );
        }
    }
}
