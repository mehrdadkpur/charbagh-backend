using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetByAssignment;

public sealed class GetLessonSessionsByAssignmentHandler
    : IRequestHandler<GetLessonSessionsByAssignmentQuery, IReadOnlyList<LessonSessionDto>>
{
    private readonly ILessonSessionRepository _repository;

    public GetLessonSessionsByAssignmentHandler(ILessonSessionRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<LessonSessionDto>> Handle(
        GetLessonSessionsByAssignmentQuery request,
        CancellationToken cancellationToken)
    {
        var sessions = await _repository.GetByLessonAssignmentIdAsync(request.LessonAssignmentId, cancellationToken);
        return sessions.Select(item => item.ToDto()).ToList();
    }
}
