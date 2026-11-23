using Charbagh.Application.Branches.Common;
using MediatR;

namespace Charbagh.Application.Branches.GetAll;

    public sealed record GetAllBranchesQuery : IRequest<IReadOnlyList<BranchDto>>;