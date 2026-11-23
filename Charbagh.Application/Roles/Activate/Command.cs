using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Activate
{
    public sealed record ActivateRoleCommand(Guid Id): IRequest<RoleDto>;    
}
