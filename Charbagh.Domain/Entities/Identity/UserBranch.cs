using Charbagh.Domain.Entities.Branches;

namespace Charbagh.Domain.Entities.Identity;

public sealed class UserBranch : BaseEntity
{
    public Guid UserId { get; private set; }

    public Guid BranchId { get; private set; }

    public bool IsDefault { get; private set; }

    public bool IsActive { get; private set; }

    public DateTime AssignedAt { get; private set; }

    public Guid? AssignedBy { get; private set; }

    // Navigation Properties
    public User User { get; private set; } = null!;

    public Branch Branch { get; private set; } = null!;

    public User? AssignedByUser { get; private set; }

    public ICollection<UserBranchRole> UserBranchRoles { get; private set; } = new List<UserBranchRole>();

    private UserBranch()
    {
        // Required by EF Core
    }

    private UserBranch(
        Guid userId,
        Guid branchId,
        Guid? assignedBy)
    {
        UserId = userId;
        BranchId = branchId;
        AssignedBy = assignedBy;

        IsActive = true;
        IsDefault = false;
        AssignedAt = DateTime.UtcNow;
    }

    public static UserBranch Create(
        Guid userId,
        Guid branchId,
        Guid? assignedBy = null)
    {
        ValidateIds(userId, branchId, assignedBy);

        var userBranch = new UserBranch(
            userId,
            branchId,
            assignedBy);

        userBranch.MarkAsCreated();

        return userBranch;
    }

    public void SetAsDefault()
    {
        if (!IsActive)
        {
            throw new InvalidOperationException(
                "An inactive user branch cannot be set as default.");
        }

        if (IsDefault)
            return;

        IsDefault = true;
        MarkAsUpdated();
    }

    public void RemoveFromDefault()
    {
        if (!IsDefault)
            return;

        IsDefault = false;
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
        if (!IsActive)
            return;

        IsActive = false;
        IsDefault = false;

        MarkAsUpdated();
    }

    private static void ValidateIds(
        Guid userId,
        Guid branchId,
        Guid? assignedBy)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                "User id is required.",
                nameof(userId));
        }

        if (branchId == Guid.Empty)
        {
            throw new ArgumentException(
                "Branch id is required.",
                nameof(branchId));
        }

        if (assignedBy.HasValue &&
            assignedBy.Value == Guid.Empty)
        {
            throw new ArgumentException(
                "Assigned by user id is invalid.",
                nameof(assignedBy));
        }
    }
}