using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetByUserId
{
    public sealed record GetUserBranchRolesByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList< UserBranchRoleDto>>;
}
