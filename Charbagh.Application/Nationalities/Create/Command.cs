using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.Create
{
    public sealed record CreateNationalityCommand( string EnglishCountryName, string PersianCountryName , string Code ):IRequest<NationalityDto>;

}
