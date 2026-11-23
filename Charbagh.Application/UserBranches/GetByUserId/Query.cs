using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetByUserId
{
    public sealed record GetUserBranchesByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList< UserBranchDto>>;
}
