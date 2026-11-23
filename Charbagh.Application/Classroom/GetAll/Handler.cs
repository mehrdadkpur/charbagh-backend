using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetAll;

public sealed class GetAllClassroomsHandler: IRequestHandler< GetAllClassroomsQuery,IReadOnlyList<ClassroomDto>>
{
    private readonly IClassroomRepository _classroomRepository;

    public GetAllClassroomsHandler( IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<IReadOnlyList<ClassroomDto>> Handle(GetAllClassroomsQuery request, CancellationToken cancellationToken)
    {
        var classrooms = await _classroomRepository.GetAllAsync(cancellationToken);

        return classrooms.Select(x => x.ToDto()).ToList();
    }
}