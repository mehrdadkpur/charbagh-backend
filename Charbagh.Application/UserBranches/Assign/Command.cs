using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.Assign
{
    public sealed record AssignUserToBranchCommand(
        Guid UserId,
        Guid BranchId,
        bool IsDefault,
        Guid? AssignedBy
        ) :IRequest<UserBranchDto>;
}
