
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.GetById
{
    public sealed record GetNationalityByIdQuery(Guid Id): IRequest<NationalityDto>;
 
}
