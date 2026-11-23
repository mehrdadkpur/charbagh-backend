namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateStudentParentException : Exception
{
    public DuplicateStudentParentException(Guid StudentId , Guid ParentId , CancellationToken cancellationToken)
        : base($"The StudentParent With Student Id '{StudentId}' And Parent Id '{ParentId}' already exists.")
    {
    }
}