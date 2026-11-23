
namespace Charbagh.Application.UserBranchRoles.Common
{
    public sealed record UserBranchRoleDto(
        Guid Id,
        Guid UserBranchId,
        Guid RoleId,
        bool IsActive,
        DateTime AssignedAt,
        Guid? AssignedBy
        );
}
