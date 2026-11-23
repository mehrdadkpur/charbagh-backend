using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.GetById
{
    public sealed record GetUserBranchByIdQuery(Guid Id):IRequest<UserBranchDto>;
}
