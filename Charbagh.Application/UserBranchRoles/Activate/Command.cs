using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.Activate
{
    public sealed record ActivateUserBranchRoleCommand(Guid Id) : IRequest<UserBranchRoleDto>;
}
