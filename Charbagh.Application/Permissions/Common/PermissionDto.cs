namespace Charbagh.Application.Permissions.Common
{
    public sealed record PermissionDto(
        Guid Id,
        string Module,
        string Name,
        PermissionAction Action,
        string DisplayName,
        string? Description,
        bool IsSystem
        );
}
