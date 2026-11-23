
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.Create
{
    public sealed record CreatePermissionCommand(
        string Module,
        string Name,
        PermissionAction Action,
        string DisplayName,
        string? Description,
        bool IsSystem
        ) : IRequest<PermissionDto>;
}
