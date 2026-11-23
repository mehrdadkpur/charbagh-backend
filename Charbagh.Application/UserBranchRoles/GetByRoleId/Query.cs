using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetByRoleId
{
    public sealed record GetUserBranchRolesByRoleIdQuery(Guid RoleId) : IRequest<IReadOnlyList< UserBranchRoleDto>>;
}
