using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.Create;

public sealed record CreateBranchCommand( string Name, string? Code, string? PhoneNumber, string? Address) : IRequest<BranchDto>;