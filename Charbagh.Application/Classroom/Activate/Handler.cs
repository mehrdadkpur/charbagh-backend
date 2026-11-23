using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.Activate;

public sealed class ActivateClassroomHandler: IRequestHandler<ActivateClassroomCommand, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateClassroomHandler(IClassroomRepository classroomRepository,IUnitOfWork unitOfWork)
    {
        _classroomRepository = classroomRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ClassroomDto> Handle(ActivateClassroomCommand request,CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (classroom is null)
            throw new KeyNotFoundException(
                "Classroom not found.");

        classroom.Activate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return classroom.ToDto();
    }
}