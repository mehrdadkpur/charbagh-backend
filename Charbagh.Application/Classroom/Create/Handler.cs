using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Domain.Entities.Branches;
using MediatR;

namespace Charbagh.Application.Classrooms.Create;

public sealed class CreateClassroomHandler: IRequestHandler<CreateClassroomCommand, ClassroomDto>
{
    private readonly IBranchRepository _branchRepository;
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateClassroomHandler( IBranchRepository branchRepository, IClassroomRepository classroomRepository, IUnitOfWork unitOfWork)
    {
        _branchRepository = branchRepository;
        _classroomRepository = classroomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClassroomDto> Handle( CreateClassroomCommand request,CancellationToken cancellationToken)
    {
        var branch = await _branchRepository.GetByIdAsync( request.BranchId, cancellationToken);

        if (branch is null)
            throw new BranchNotFoundException(request.BranchId);

        var exists = await _classroomRepository.ExistsByCodeAsync(request.BranchId, request.Code, cancellationToken);

        if (exists)
            throw new InvalidOperationException("A classroom with this code already exists in this branch.");

        var classroom = Classroom.Create(
            request.BranchId,
            request.Name,
            request.Code,
            request.Capacity,
            request.Description);

        await _classroomRepository.AddAsync(
            classroom,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return classroom.ToDto();
    }
}