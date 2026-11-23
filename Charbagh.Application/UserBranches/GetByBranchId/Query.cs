using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetByBranchId
{
    public sealed record GetUserBranchesByBranchIdQuery(Guid BranchId) : IRequest<IReadOnlyList< UserBranchDto>>;
}
