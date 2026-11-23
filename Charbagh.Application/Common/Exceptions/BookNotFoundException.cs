namespace Charbagh.Application.Common.Exceptions;

public sealed class BookNotFoundException : Exception
{
    public BookNotFoundException(Guid BookId)
        : base($"This Book '{BookId}' Not Found")
    {
    }
}