using MediatR;

namespace Charbagh.Application.Roles.AssignPermission;

public sealed record AddPermissionToRoleCommand(
    Guid RoleId,
    Guid PermissionId
) : IRequest;