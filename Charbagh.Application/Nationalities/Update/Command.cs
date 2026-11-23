using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.Update
{
    public sealed record UpdateNationalityCommand(Guid Id , string EnglishCountryName, string PersianCountryName , string Code): IRequest<NationalityDto>;
        
}
