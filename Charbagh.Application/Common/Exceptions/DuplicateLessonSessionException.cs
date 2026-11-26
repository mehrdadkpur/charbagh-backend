namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateLessonSessionException : Exception
{
    public DuplicateLessonSessionException(Guid lessonAssignmentId, DateTime sessionDate)
        : base($"برای این تخصیص آموزشی در تاریخ {sessionDate:O} جلسه‌ای ثبت شده است.")
    {
        LessonAssignmentId = lessonAssignmentId;
        SessionDate = sessionDate;
    }

    public Guid LessonAssignmentId { get; }
    public DateTime SessionDate { get; }
}
