namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateLessonAssignmentException : Exception
{
    public DuplicateLessonAssignmentException(Guid StudentId , Guid InstrumentId)
        : base($"The Lesson Assignment With Student Id  '{StudentId}' And Instrument Id '{InstrumentId}' already exists.")
    {
    }
}