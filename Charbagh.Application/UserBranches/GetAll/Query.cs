using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetAll
{
    public sealed record GetAllUserBranchesQuery() : IRequest<IReadOnlyList< UserBranchDto>>;
}
