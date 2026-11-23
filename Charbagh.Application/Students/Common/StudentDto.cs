

namespace Charbagh.Application.Students.Common
{
    public sealed record StudentDto(
        Guid Id,
        Guid UserId,
        string FirstName,
        string LastName,
        string NationalIdentificationNumber,
        DateOnly? BirthDate,
        string? Email,
        string? ProfileImageUrl,
        string StudentCode , 
        DateOnly RegistrationDate , 
        StudentStatus Status
        );

}
