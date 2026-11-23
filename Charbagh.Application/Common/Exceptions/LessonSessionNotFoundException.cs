namespace Charbagh.Application.Common.Exceptions;

public sealed class LessonSessionNotFoundException : Exception
{
    public LessonSessionNotFoundException(Guid Id)
        : base($"This Lesson Session {Id} Not Found")
    {
    }
}