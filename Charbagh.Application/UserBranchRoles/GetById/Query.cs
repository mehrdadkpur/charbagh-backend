using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetById
{
    public sealed record GetUserBranchRoleByIdQuery(Guid Id):IRequest<UserBranchRoleDto>;
}
