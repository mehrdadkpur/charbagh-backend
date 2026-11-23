namespace Charbagh.Application.Common.Exceptions;

public sealed class PracticeAssignmentNotFoundException : Exception
{
    public PracticeAssignmentNotFoundException(Guid Id)
        : base($"This Practice Assignment '{Id}' Not Found")
    {
    }
}