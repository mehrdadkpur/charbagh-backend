
namespace Charbagh.Application.UserBranches.Common
{
    public sealed record UserBranchDto(
        Guid Id,
        Guid UserId,
        Guid BranchId,
        bool IsDefault,
        bool IsActive,
        DateTime AssignedAt,
        Guid? AssignedBy
        );
}
