
using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Suspend
{
    public sealed record SuspendUserCommand(Guid id) : IRequest<UserDto>;
}
