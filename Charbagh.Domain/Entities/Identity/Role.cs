namespace Charbagh.Domain.Entities.Identity;

public sealed class Role : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string DisplayName { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public bool IsSystem { get; private set; }

    public ICollection<UserBranchRole> UserBranchRoles { get; private set; } = [];

    public ICollection<RolePermission> RolePermissions { get; private set; } =[];

    private Role()
    {
    }

    private Role(
        string name,
        string displayName,
        string? description,
        bool isSystem)
    {
        Name = name;
        DisplayName = displayName;
        Description = description;
        IsActive = true;
        IsSystem = isSystem;
    }

    public static Role Create(
        string name,
        string displayName,
        string? description = null,
        bool isSystem = false)
    {
        Validate(name, displayName);

        var role = new Role(
            name.Trim(),
            displayName.Trim(),
            description?.Trim(),
            isSystem);

        role.MarkAsCreated();

        return role;
    }

    public void Update(
        string name,
        string displayName,
        string? description)
    {
        if (IsSystem)
        {
            throw new InvalidOperationException(
                "System roles cannot be updated.");
        }

        Validate(name, displayName);

        Name = name.Trim();
        DisplayName = displayName.Trim();
        Description = description?.Trim();

        MarkAsUpdated();
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        MarkAsUpdated();
    }

    public void Deactivate()
    {
        if (IsSystem)
            throw new InvalidOperationException("System roles cannot be deactivated.");
        if (!IsActive)
            return;

        IsActive = false;
        MarkAsUpdated();
    }

    private static void Validate(
        string name,
        string displayName)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Role name is required.",
                nameof(name));
        }

        if (string.IsNullOrWhiteSpace(displayName))
        {
            throw new ArgumentException(
                "Role display name is required.",
                nameof(displayName));
        }

        if (name.Trim().Length > 100)
        {
            throw new ArgumentException(
                "Role name cannot be longer than 100 characters.",
                nameof(name));
        }

        if (displayName.Trim().Length > 150)
        {
            throw new ArgumentException(
                "Role display name cannot be longer than 150 characters.",
                nameof(displayName));
        }
    }
}