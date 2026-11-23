namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateParentException : Exception
{
    public DuplicateParentException()
        : base($"The Parent already exists.")
    {
    }
}