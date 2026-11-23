using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.Activate
{
    public sealed record ActivateUserBranchCommand(Guid Id) : IRequest<UserBranchDto>;
}
