namespace Charbagh.Application.Roles.Common
{
    public sealed record RoleDto(
        Guid Id,
        string Name,
        string DisplayName,
        string? Description,
        bool IsSystem,
        bool IsActive
        );
}
