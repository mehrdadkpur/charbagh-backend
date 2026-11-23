

using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Unblock
{
    public sealed record UnblockUserCommand(Guid Id): IRequest<UserDto>;    
}
