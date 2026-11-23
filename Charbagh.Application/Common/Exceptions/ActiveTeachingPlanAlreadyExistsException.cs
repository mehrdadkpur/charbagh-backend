namespace Charbagh.Application.Common.Exceptions;

public sealed class ActiveTeachingPlanAlreadyExistsException : Exception
{
    public ActiveTeachingPlanAlreadyExistsException(Guid LessonAssignmentId)
        : base($"The LessonAssignment Id '{LessonAssignmentId}' already has been Active.")
    {
    }
}