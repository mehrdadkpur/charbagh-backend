using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;
using Charbagh.Application.Branches.Deactivate;
using Charbagh.Application.Common.Exceptions;
using MediatR;

public sealed class DeactivateBranchHandler : IRequestHandler<DeactivateBranchCommand, BranchDto>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateBranchHandler( IBranchRepository branchRepository,IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle(DeactivateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync( request.Id, cancellationToken);

        if (branch is null)
            throw new BranchNotFoundException(request.Id);

        if (branch.IsActive)
        {
            branch.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);
        }

        return branch.ToDto();
    }
}