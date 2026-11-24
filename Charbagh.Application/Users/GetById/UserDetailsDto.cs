using Charbagh.Application.Users.Common;

namespace Charbagh.Application.Users.GetById;

public sealed record UserDetailsDto(
    UserDto User,
    UserProfileDetailsDto? Student,
    UserProfileDetailsDto? Teacher,
    UserProfileDetailsDto? Parent,
    UserProfileDetailsDto? Staff,
    IReadOnlyList<UserBranchDetailsDto> Branches);

public sealed record UserProfileDetailsDto(
    Guid Id,
    string Code,
    int Status,
    string? SecondaryCode = null);

public sealed record UserBranchDetailsDto(
    Guid Id,
    Guid BranchId,
    string BranchName,
    string? BranchCode,
    bool IsDefault,
    bool IsActive,
    DateTime AssignedAt,
    IReadOnlyList<UserRoleDetailsDto> Roles);

public sealed record UserRoleDetailsDto(
    Guid Id,
    string Name,
    string DisplayName,
    bool IsActive,
    DateTime AssignedAt);
