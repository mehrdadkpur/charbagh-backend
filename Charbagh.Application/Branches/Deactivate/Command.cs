using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.Deactivate;

public sealed record DeactivateBranchCommand(Guid Id) : IRequest<BranchDto>;