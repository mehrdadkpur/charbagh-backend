

using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Activate
{
    public sealed record ActivateUserCommand(Guid Id): IRequest<UserDto>;    
}
