namespace Charbagh.Application.Common.Exceptions;

public sealed class LessonAssignmentNotFoundException : Exception
{
    public LessonAssignmentNotFoundException(Guid lessonAssignmentId)
        : base($"This Lesson Assignment '{lessonAssignmentId}' Not Found")
    {
    }
}