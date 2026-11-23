using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.GetById;

public sealed record GetBranchByIdQuery(Guid Id) : IRequest<BranchDto>;