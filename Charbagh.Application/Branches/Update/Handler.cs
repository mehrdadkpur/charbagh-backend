using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Branches.Update;

public sealed class UpdateBranchHandler: IRequestHandler<UpdateBranchCommand, BranchDto>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateBranchHandler( IBranchRepository branchRepository, IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle( UpdateBranchCommand request, CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync( request.Id, cancellationToken);
            if (branch is null)
            {
                throw new BranchNotFoundException(request.Id);
            }

        var existing = await _branchRepository.GetByCodeAsync( request.Code, cancellationToken);

        if (existing is not null && existing.Id != request.Id)
        {
            throw new DuplicateBranchCodeException(request.Code!);
        }

        branch.Update(
            request.Name,
            request.Code,
            request.PhoneNumber,
            request.Address);


        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return branch.ToDto();
    }
}