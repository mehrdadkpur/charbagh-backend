namespace Charbagh.Application.Common.Exceptions;

public sealed class CommentNotFoundException : Exception
{
    public CommentNotFoundException(Guid Id)
        : base($"This Comment {Id} Not Found")
    {
    }
}