namespace Charbagh.Domain.Entities.Identity;

public sealed class RolePermission
{
    public Guid RoleId { get; private set; }

    public Guid PermissionId { get; private set; }

    public Role Role { get; private set; } = null!;

    public Permission Permission { get; private set; } = null!;

    private RolePermission()
    {
    }

    private RolePermission(
        Guid roleId,
        Guid permissionId)
    {
        RoleId = roleId;
        PermissionId = permissionId;
    }

    public static RolePermission Create(
        Guid roleId,
        Guid permissionId)
    {
        if (roleId == Guid.Empty)
        {
            throw new ArgumentException(
                "Role id is required.",
                nameof(roleId));
        }

        if (permissionId == Guid.Empty)
        {
            throw new ArgumentException(
                "Permission id is required.",
                nameof(permissionId));
        }

        return new RolePermission(
            roleId,
            permissionId);
    }
}