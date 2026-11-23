

namespace Charbagh.Application.Nationalities.Common
{
    public sealed record NationalityDto (Guid Id , string EnglishCountryName, string PersianCountryName , string Code , NationalityStatus Status);

}
