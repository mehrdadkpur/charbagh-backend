using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.GetByAssignment;

public sealed record GetLessonSessionsByAssignmentQuery(Guid LessonAssignmentId)
    : IRequest<IReadOnlyList<LessonSessionDto>>;
