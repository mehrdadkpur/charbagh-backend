using Charbagh.Domain.Entities.Identity;
namespace Charbagh.Application.UserBranchRoles.Common
{
    public static class UserBranchRoleMapper
    {
        public static UserBranchRoleDto ToDto(this UserBranchRole userBranchRole)
        {
            return new UserBranchRoleDto(
                userBranchRole.Id,
                userBranchRole.UserBranchId,
                userBranchRole.RoleId,
                userBranchRole.IsActive,
                userBranchRole.AssignedAt,
                userBranchRole.AssignedBy);
        }
    }
}
