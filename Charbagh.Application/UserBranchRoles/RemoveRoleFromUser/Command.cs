using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.RemoveRoleFromUser
{
    public sealed record RemoveRoleFromUserCommand(Guid Id) : IRequest<UserBranchRoleDto>;
}
