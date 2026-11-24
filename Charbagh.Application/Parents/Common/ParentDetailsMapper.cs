using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Parents.Common;

public static class ParentDetailsMapper
{
    public static ParentDetailsDto ToDetailsDto(this Parent parent)
    {
        ArgumentNullException.ThrowIfNull(parent);

        var user = parent.User
            ?? throw new InvalidOperationException($"User navigation property is null for Parent: {parent.Id}");

        var children = parent.StudentParents
            .OrderBy(x => x.Student.User.LastName)
            .ThenBy(x => x.Student.User.FirstName)
            .Select(x => new ParentStudentDetailsDto(
                x.StudentId,
                x.Student.User.FirstName,
                x.Student.User.LastName,
                x.Student.StudentCode,
                x.Student.User.BirthDate,
                x.Student.User.ProfileImageUrl,
                x.Student.Status,
                x.Relationship))
            .ToList();

        return new ParentDetailsDto(
            parent.Id,
            parent.UserId,
            user.FirstName,
            user.LastName,
            user.Email,
            user.NationalIdentificationNumber,
            user.ProfileImageUrl,
            parent.Status,
            user.Bio,
            children);
    }
}
