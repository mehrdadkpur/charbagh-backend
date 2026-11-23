using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetAll
{
    public sealed record GetAllUserBranchRolesQuery() : IRequest<IReadOnlyList< UserBranchRoleDto>>;
}
