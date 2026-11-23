namespace Charbagh.Application.Common.Exceptions;

public sealed class LessonContentNotFoundException : Exception
{
    public LessonContentNotFoundException(Guid LessonContentId)
        : base($"This Lesson Content '{LessonContentId}' Not Found")
    {
    }
}