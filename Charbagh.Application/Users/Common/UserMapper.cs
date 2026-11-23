
namespace Charbagh.Application.Users.Common
{
    public static class UserMapper
    {
        public static UserDto ToDto(this Charbagh.Domain.Entities.Identity.User user)
        {
            return new UserDto(
                user.Id,
                user.FirstName,
                user.LastName,
                user.Mobile,
                user.NationalIdentificationNumber,
                user.Email,
                user.BirthDate,
                user.Gender,
                user.Address,
                user.Bio,
                user.ProfileImageUrl,
                user.FullName,
                user.Status,
                user.CreatedAt,
                Nationality: user.Nationality is null ? null : new NationalityDto( user.Nationality.Id, user.Nationality.EnglishCountryName , user.Nationality.PersianCountryName)
                );
        }
    }
}
