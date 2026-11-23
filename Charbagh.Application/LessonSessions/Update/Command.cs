
using Charbagh.Application.LessonSessions.Common;
using MediatR;

namespace Charbagh.Application.LessonSessions.Update
{
    public sealed record UpdateLessonSessionCommand(
        Guid Id,
        Guid LessonAssignmentId,
        DateTime SessionDate,
        TimeSpan? StartTime,
        TimeSpan? EndTime,
        int? DurationMinutes,
        string TeacherNote
        ): IRequest<LessonSessionDto>;
    
}
