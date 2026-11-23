using Charbagh.Domain.Entities.Identity;
namespace Charbagh.Application.UserBranches.Common
{
    public static class UserBranchMapper
    {
        public static UserBranchDto ToDto(this UserBranch userBranch)
        {
            return new UserBranchDto(
                userBranch.Id,
                userBranch.UserId,
                userBranch.BranchId,
                userBranch.IsDefault,
                userBranch.IsActive,
                userBranch.AssignedAt,
                userBranch.AssignedBy);
        }
    }
}
