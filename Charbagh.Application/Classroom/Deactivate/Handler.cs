using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Deactivate;

public sealed class DeactivateClassroomHandler: IRequestHandler<DeactivateClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateClassroomHandler(
        IClassroomRepository classroomRepository,
        IUnitOfWork unitOfWork)
    {
        _classroomRepository = classroomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClassroomDto> Handle(DeactivateClassroomCommand request,CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (classroom is null)
            throw new KeyNotFoundException(
                "Classroom not found.");

        classroom.Deactivate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return classroom.ToDto();
    }
}