using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.RemoveUserFromBranch
{
    public sealed record RemoveUserFromBranchCommand(Guid Id) : IRequest<UserBranchDto>;
}
