using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.GetAll
{
    public sealed record GetAllNationalitesQuery: IRequest<IReadOnlyList<NationalityDto>>;    
}
