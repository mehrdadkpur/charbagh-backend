
using Charbagh.Application.Nationalities.Common;
using MediatR;

namespace Charbagh.Application.Users.Deactivate
{
    public sealed record DeactivateNationalityCommand(Guid id) : IRequest<NationalityDto>;
}
