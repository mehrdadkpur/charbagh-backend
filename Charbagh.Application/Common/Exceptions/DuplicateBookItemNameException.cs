namespace Charbagh.Application.Common.Exceptions;

public sealed class DuplicateBookItemNameException : Exception
{
    public DuplicateBookItemNameException(string BookItemName)
        : base($"The Book Item '{BookItemName}' already exists.")
    {
    }
}