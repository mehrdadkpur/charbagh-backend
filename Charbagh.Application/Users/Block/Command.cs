
using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Block
{
    public sealed record BlockUserCommand(Guid id) : IRequest<UserDto>;
}
