using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Deactivate
{
    public sealed record DeactivateRoleCommand(Guid Id): IRequest<RoleDto>;    
}
