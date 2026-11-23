using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Nationalities.Common
{
    public static class NationalityMapper
    {
        public static NationalityDto ToDto(this Nationality nationality)
        {
            return new NationalityDto(
                nationality.Id,
                nationality.EnglishCountryName,
                nationality.PersianCountryName,
                nationality.Code,
                nationality.Status
                );
        }
    }
}
