namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateBookNameException : Exception
{
    public DuplicateBookNameException(string BookTitle)
        : base($"The Book '{BookTitle}' already exists.")
    {
    }
}