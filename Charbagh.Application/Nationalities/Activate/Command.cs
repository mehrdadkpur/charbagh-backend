using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Nationalities.Activate
{
    public sealed record ActivateNationalityCommand(Guid id):IRequest<NationalityDto>;
}
