
namespace Charbagh.Domain.Entities.Identity;

public sealed class Permission : BaseEntity
{
    public string Module { get; private set; } = null!;

    public string Name { get; private set; } = null!;

    public PermissionAction Action { get; private set; }

    public string DisplayName { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsSystem { get; private set; }

    public ICollection<RolePermission> RolePermissions { get; private set; } = new List<RolePermission>();

    private Permission()
    {
    }

    private Permission(
        string module,
        string name,
        PermissionAction action,
        string displayName,
        string? description,
        bool isSystem)
    {
        Module = module;
        Name = name;
        Action = action;
        DisplayName = displayName;
        Description = description;
        IsSystem = isSystem;
    }

    public static Permission Create(
        string module,
        string name,
        PermissionAction action,
        string displayName,
        string? description = null,
        bool isSystem = true)
    {
        Validate(
            module,
            name,
            displayName);

        var permission = new Permission(
            module.Trim(),
            name.Trim(),
            action,
            displayName.Trim(),
            description?.Trim(),
            isSystem);

        permission.MarkAsCreated();

        return permission;
    }

    public void Update(
        string module,
        string name,
        PermissionAction action,
        string displayName,
        string? description)
    {
        if (IsSystem)
            throw new InvalidOperationException(
                "System permissions cannot be updated.");

        Validate(
            module,
            name,
            displayName);

        Module = module.Trim();
        Name = name.Trim();
        Action = action;
        DisplayName = displayName.Trim();
        Description = description?.Trim();

        MarkAsUpdated();
    }

    private static void Validate(
        string module,
        string name,
        string displayName)
    {
        if (string.IsNullOrWhiteSpace(module))
            throw new ArgumentException(
                "Permission module is required.",
                nameof(module));

        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Permission name is required.",
                nameof(name));

        if (string.IsNullOrWhiteSpace(displayName))
            throw new ArgumentException(
                "Permission display name is required.",
                nameof(displayName));

        if (module.Trim().Length > 100)
            throw new ArgumentException(
                "Permission module cannot be longer than 100 characters.",
                nameof(module));

        if (name.Trim().Length > 100)
            throw new ArgumentException(
                "Permission name cannot be longer than 100 characters.",
                nameof(name));

        if (displayName.Trim().Length > 150)
            throw new ArgumentException(
                "Permission display name cannot be longer than 150 characters.",
                nameof(displayName));
    }
}