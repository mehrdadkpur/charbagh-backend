using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.Update
{
    public sealed record UpdatePermissionCommand(
        Guid Id,
        string Module,
        string Name,
        PermissionAction Action,
        string DisplayName,
        string? Description,
        bool IsSystem
        ) :IRequest<PermissionDto>;
}
