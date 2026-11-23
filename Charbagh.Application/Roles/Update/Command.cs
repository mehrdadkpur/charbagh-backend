
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Update
{
    public sealed record UpdateRoleCommand(
        Guid Id,
        string Name,
        string DisplayName,
        string? Description,
        bool IsSystem
        ) :IRequest<RoleDto>
    {
    }
}
