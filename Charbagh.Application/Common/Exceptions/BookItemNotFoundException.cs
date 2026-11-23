namespace Charbagh.Application.Common.Exceptions;

public sealed class BookItemNotFoundException : Exception
{
    public BookItemNotFoundException(Guid bookitemId)
        : base($"This BookItem '{bookitemId}' Not Found")
    {
    }
}