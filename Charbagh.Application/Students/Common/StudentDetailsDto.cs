namespace Charbagh.Application.Students.Common;

public sealed record StudentDetailsDto(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName,
    string Mobile,
    string NationalIdentificationNumber,
    DateOnly? BirthDate,
    Gender Gender,
    string? Email,
    string? ProfileImageUrl,
    string? Address,
    string? Nationality,
    string? Biography,
    string StudentCode,
    DateOnly RegistrationDate,
    StudentStatus Status,
    IReadOnlyList<StudentParentDetailsDto> Parents);

public sealed record StudentParentDetailsDto(
    Guid Id,
    Guid ParentId,
    StudentParentRelationship Relationship,
    string FirstName,
    string LastName,
    string? NationalIdentificationNumber,
    string? Mobile,
    string? ProfileImageUrl,
    string? Email);
