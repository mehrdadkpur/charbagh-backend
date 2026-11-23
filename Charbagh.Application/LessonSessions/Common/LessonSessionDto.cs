
namespace Charbagh.Application.LessonSessions.Common
{
    public sealed record LessonSessionDto(
        Guid Id,
        Guid LessonAssignmentId,
        DateTime SessionDate,
        TimeSpan? StartTime,
        TimeSpan? EndTime,
        int? DurationMinutes,
        string? TeacherNote,
        LessonSessionStatus Status
        );
}
