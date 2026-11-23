using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;
using Charbagh.Application.Branches.GetById;
using Charbagh.Application.Common.Exceptions;
using MediatR;

public sealed class GetBranchByIdHandler: IRequestHandler<GetBranchByIdQuery, BranchDto>
{
    private readonly IBranchRepository _repository;

    public GetBranchByIdHandler(IBranchRepository repository)
    {
        _repository = repository;
    }

    public async Task<BranchDto> Handle(GetBranchByIdQuery request, CancellationToken cancellationToken)
    {
        var branch = await _repository.GetByIdAsync( request.Id,cancellationToken);

        if (branch is null)
            throw new BranchNotFoundException(request.Id);

        return branch.ToDto();
    }
}