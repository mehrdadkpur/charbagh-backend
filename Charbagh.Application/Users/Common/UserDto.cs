
namespace Charbagh.Application.Users.Common
{

    public sealed record NationalityDto(Guid Id, string EnglishCountryName, string PersianCountryName);

    public sealed record UserDto(
        Guid Id, 
        string FirstName, 
        string LastName, 
        string Mobile, 
        string NationalIdentificationNumber,
        string? Email,
        DateOnly? BirthDate, 
        Gender Gender, 
        string? Address, 
        string? Bio, 
        string? ProfileImageUrl,
        string FullName,
        UserStatus Status,
        DateTime CreatedAt,
        NationalityDto? Nationality
        );

}
