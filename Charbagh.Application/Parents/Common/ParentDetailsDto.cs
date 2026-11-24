namespace Charbagh.Application.Parents.Common;

public sealed record ParentDetailsDto(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName,
    string? Email,
    string NationalIdentificationNumber,
    string? ProfileImageUrl,
    ParentStatus Status,
    string? Bio,
    IReadOnlyList<ParentStudentDetailsDto> Children);

public sealed record ParentStudentDetailsDto(
    Guid StudentId,
    string FirstName,
    string LastName,
    string StudentCode,
    DateOnly? BirthDate,
    string? ProfileImageUrl,
    StudentStatus Status,
    StudentParentRelationship Relationship);
