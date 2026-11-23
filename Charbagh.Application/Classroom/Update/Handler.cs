using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Update;

public sealed class UpdateClassroomHandler: IRequestHandler<UpdateClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateClassroomHandler(IClassroomRepository classroomRepository,IUnitOfWork unitOfWork)
    {
        _classroomRepository = classroomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClassroomDto> Handle(UpdateClassroomCommand request,CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetForUpdateAsync( request.Id, cancellationToken);

        if (classroom is null)
            throw new KeyNotFoundException("Classroom not found.");

        if (!string.Equals(
                classroom.Code,
                request.Code,
                StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _classroomRepository.ExistsByCodeAsync(
                classroom.BranchId,
                request.Code,
                cancellationToken);

            if (exists)
                throw new InvalidOperationException(
                    "A classroom with this code already exists in this branch.");
        }

        classroom.Update(
            request.Name,
            request.Code,
            request.Capacity,
            request.Description);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return classroom.ToDto();
    }
}