using Charbagh.Domain.Entities.Education.StudentEducation;
namespace Charbagh.Application.LessonSessions.Common
{
    public static class LessonSessionMapper
    {
        public static LessonSessionDto ToDto(this LessonSession lessonSession)
        {
            return new LessonSessionDto(
                lessonSession.Id,
                lessonSession.LessonAssignmentId,
                lessonSession.SessionDate,
                lessonSession.StartTime,
                lessonSession.EndTime,
                lessonSession.DurationMinutes,
                lessonSession.TeacherNote,
                lessonSession.Status
                );
        }
    }
}
