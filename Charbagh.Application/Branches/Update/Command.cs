using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.Update;

public sealed record UpdateBranchCommand(
    Guid Id,
    string Name,
    string? Code,
    string? PhoneNumber,
    string? Address
) : IRequest<BranchDto>;