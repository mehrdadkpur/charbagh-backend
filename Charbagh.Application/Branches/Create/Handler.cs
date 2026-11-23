using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Branches.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Branches;
using MediatR;

namespace Charbagh.Application.Branches.Create;

public sealed class CreateBranchHandler : IRequestHandler<CreateBranchCommand, BranchDto>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateBranchHandler( IBranchRepository branchRepository,IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<BranchDto> Handle( CreateBranchCommand request, CancellationToken cancellationToken)
    {
        var existingBranch = await _branchRepository.GetByCodeAsync( request.Code, cancellationToken);

        if (existingBranch is not null)
        {
            throw new DuplicateBranchCodeException(request.Code!);
        }

        var branch = Branch.Create(
            request.Name,
            request.Code,
            request.PhoneNumber,
            request.Address);

        await _branchRepository.AddAsync(branch, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return branch.ToDto();
    }
}