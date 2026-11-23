using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Branches.Activate;

public sealed class ActivateBranchHandler: IRequestHandler<ActivateBranchCommand, BranchDto>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateBranchHandler( IBranchRepository branchRepository, IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle( ActivateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync( request.Id,cancellationToken);

        if (branch is null)
            throw new BranchNotFoundException(request.Id);

        if (!branch.IsActive)
        {
            branch.Activate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return branch.ToDto();
    }
}