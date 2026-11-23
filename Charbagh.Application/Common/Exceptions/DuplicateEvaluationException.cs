namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateEvaluationException : Exception
{
    public DuplicateEvaluationException(Guid Id)
        : base($"The Evaluation With Lesson Session Id '{Id}' already exists.")
    {
    }
}