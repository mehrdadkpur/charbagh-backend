using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Classrooms.Common;
using MediatR;

namespace Charbagh.Application.Classrooms.GetByBranchId;

public sealed class GetClassroomsByBranchIdHandler: IRequestHandler<GetClassroomsByBranchIdQuery,IReadOnlyList<ClassroomDto>>
{
    private readonly IClassroomRepository _classroomRepository;

    public GetClassroomsByBranchIdHandler(IClassroomRepository classroomRepository)
    {
        _classroomRepository = classroomRepository;
    }

    public async Task<IReadOnlyList<ClassroomDto>> Handle(GetClassroomsByBranchIdQuery request,CancellationToken cancellationToken)
    {
        var classrooms = await _classroomRepository.GetByBranchIdAsync(request.BranchId,cancellationToken);

        return classrooms.Select(x => x.ToDto()).ToList();
    }
}