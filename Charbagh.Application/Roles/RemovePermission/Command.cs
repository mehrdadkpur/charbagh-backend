using MediatR;

namespace Charbagh.Application.Roles.RemovePermission;

public sealed record RemovePermissionFromRoleCommand(
    Guid RoleId,
    Guid PermissionId
) : IRequest;