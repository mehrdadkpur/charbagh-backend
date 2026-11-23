
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Create
{
    public sealed record CreateRoleCommand(
        string Name,
        string DisplayName,
        string? Description,
        bool IsSystem
        ) : IRequest<RoleDto>;
}
