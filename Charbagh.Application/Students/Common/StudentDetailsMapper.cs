using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Students.Common;

public static class StudentDetailsMapper
{
    public static StudentDetailsDto ToDetailsDto(this Student student)
    {
        ArgumentNullException.ThrowIfNull(student);

        var user = student.User
            ?? throw new InvalidOperationException(
                $"User navigation property is null for Student: {student.Id}");

        var parents = student.StudentParents
            .OrderBy(x => x.Parent.User.LastName)
            .ThenBy(x => x.Parent.User.FirstName)
            .Select(x => new StudentParentDetailsDto(
                x.Id,
                x.ParentId,
                x.Relationship,
                x.Parent.User.FirstName,
                x.Parent.User.LastName,
                x.Parent.User.NationalIdentificationNumber,
                x.Parent.User.Mobile,
                x.Parent.User.ProfileImageUrl,
                x.Parent.User.Email))
            .ToList();

        return new StudentDetailsDto(
            student.Id,
            student.UserId,
            user.FirstName,
            user.LastName,
            user.Mobile,
            user.NationalIdentificationNumber,
            user.BirthDate,
            user.Gender,
            user.Email,
            user.ProfileImageUrl,
            user.Address,
            user.Nationality?.PersianCountryName,
            user.Bio,
            student.StudentCode,
            student.RegistrationDate,
            student.Status,
            parents);
    }
}
