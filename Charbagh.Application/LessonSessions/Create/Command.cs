
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.Create
{
    public sealed record CreateLessonSessionCommand(
        Guid LessonAssignmentId,
        DateTime SessionDate,
        TimeSpan? StartTime,
        TimeSpan? EndTime,
        int? DurationMinutes,
        string? TeacherNote
        ) :IRequest<LessonSessionDto>;
}
