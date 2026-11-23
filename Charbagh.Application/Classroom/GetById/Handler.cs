using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetById;

public sealed class GetClassroomByIdHandler: IRequestHandler<GetClassroomByIdQuery, ClassroomDto>
{
    private readonly IClassroomRepository _classroomRepository;

    public GetClassroomByIdHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<ClassroomDto> Handle(GetClassroomByIdQuery request,CancellationToken cancellationToken)
    {
        var classroom = await _classroomRepository.GetByIdAsync( request.Id, cancellationToken);

        if (classroom is null)
            throw new KeyNotFoundException("Classroom not found.");

        return classroom.ToDto();
    }
}