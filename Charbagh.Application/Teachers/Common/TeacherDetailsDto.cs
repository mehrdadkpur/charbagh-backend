namespace Charbagh.Application.Teachers.Common;

public sealed record TeacherDetailsDto(
    Guid Id,
    Guid UserId,
    string FirstName,
    string LastName,
    string Mobile,
    string? Email,
    string? ProfileImageUrl,
    DateOnly? BirthDate,
    Gender Gender,
    string NationalIdentificationNumber,
    string? Address,
    string? UserBiography,
    string Nationality,
    string PersonnelCode,
    DateOnly EmploymentDate,
    UserEducation? Education,
    int? ExperienceYears,
    string? Biography,
    bool IsVisibleOnWebsite,
    TeacherStatus Status,
    IReadOnlyList<TeacherInstrumentDetailsDto> Instruments);

public sealed record TeacherInstrumentDetailsDto(
    Guid Id,
    Guid InstrumentId,
    string InstrumentName,
    string? EnglishName,
    string? ImageUrl,
    DateOnly? StartTeachingDate,
    TeachingLevel TeachingLevel,
    decimal? DefaultSessionFee,
    bool IsActive,
    Guid? DefaultBookId,
    string? DefaultBookTitle,
    string? DefaultBookAuthor);
