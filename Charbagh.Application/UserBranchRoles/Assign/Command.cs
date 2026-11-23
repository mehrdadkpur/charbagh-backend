using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.Assign
{
    public sealed record AssignCommand(
        Guid UserBranchId,
        Guid RoleId,
        bool IsDefault,
        Guid? AssignedBy
        ) :IRequest<UserBranchRoleDto>;
}
