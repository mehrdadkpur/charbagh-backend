using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;

using MediatR;

namespace Charbagh.Application.Branches.GetAll;

public sealed class GetAllBranchesHandler : IRequestHandler<GetAllBranchesQuery,IReadOnlyList<BranchDto>>
{
    private readonly IBranchRepository _repository;

    public GetAllBranchesHandler(IBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<BranchDto>> Handle(GetAllBranchesQuery request,CancellationToken cancellationToken)
    {
        var branches = await _repository.GetAllAsync(cancellationToken);

        return branches
            .Select(x => x.ToDto())
            .ToList();
    }
}