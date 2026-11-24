namespace Charbagh.Application.Common.Exceptions;

public sealed class StudentParentValidationException : Exception
{
    public StudentParentValidationException(string message) : base(message)
    {
    }
}
