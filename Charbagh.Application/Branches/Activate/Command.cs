using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.Activate;

public sealed record ActivateBranchCommand(Guid Id) : IRequest<BranchDto>;