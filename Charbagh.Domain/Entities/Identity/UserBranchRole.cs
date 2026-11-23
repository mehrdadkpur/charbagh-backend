using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Identity;

public sealed class UserBranchRole : BaseEntity
{
    public Guid UserBranchId { get; private set; }

    public Guid RoleId { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime AssignedAt { get; private set; }

    public Guid? AssignedBy { get; private set; }

    // Navigation Properties
    public UserBranch UserBranch { get; private set; } = null!;

    public Role Role { get; private set; } = null!;

    public User? AssignedByUser { get; private set; }

    private UserBranchRole()
    {
        // Required by EF Core
    }

    private UserBranchRole(
        Guid userBranchId,
        Guid roleId,
        Guid? assignedBy)
    {
        UserBranchId = userBranchId;
        RoleId = roleId;
        AssignedBy = assignedBy;

        IsActive = true;
        AssignedAt = DateTime.UtcNow;
    }

    public static UserBranchRole Create(
        Guid userBranchId,
        Guid roleId,
        Guid? assignedBy = null)
    {
        if (userBranchId == Guid.Empty)
        {
            throw new ArgumentException(
                "User branch id is required.",
                nameof(userBranchId));
        }

        if (roleId == Guid.Empty)
        {
            throw new ArgumentException(
                "Role id is required.",
                nameof(roleId));
        }

        if (assignedBy.HasValue &&
            assignedBy.Value == Guid.Empty)
        {
            throw new ArgumentException(
                "Assigned by user id is invalid.",
                nameof(assignedBy));
        }

        var userBranchRole = new UserBranchRole(
            userBranchId,
            roleId,
            assignedBy);

        userBranchRole.MarkAsCreated();

        return userBranchRole;
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
        if (!IsActive)
            return;

        IsActive = false;
        MarkAsUpdated();
    }
}