namespace Charbagh.Application.RolePermissions.Common;

public sealed record RolePermissionDto(
    Guid RoleId,
    Guid PermissionId);