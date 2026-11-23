using Charbagh.Application.Permissions.Common;

namespace Charbagh.Application.Roles.GetById;

public sealed record RoleDetailsDto(
    Guid Id,
    string Name,
    string DisplayName,
    string? Description,
    bool IsSystem,
    bool IsActive,
    DateTime CreatedAt,
    DateTime? UpdatedAt,
    int ActiveAssignmentCount,
    int UniqueUserCount,
    int BranchCount,
    IReadOnlyList<RoleUserDto> Users,
    IReadOnlyList<PermissionDto> Permissions
);

public sealed record RoleUserDto(
    Guid UserBranchRoleId,
    Guid UserBranchId,
    Guid UserId,
    string FirstName,
    string LastName,
    string Mobile,
    string? Email,
    Guid BranchId,
    string BranchName,
    bool IsUserBranchActive,
    bool IsAssignmentActive,
    DateTime AssignedAt,
    Guid? AssignedBy,
    string? AssignedByName
);