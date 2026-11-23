namespace Charbagh.Application.Common.Exceptions;

public sealed class BlogNotFoundException : Exception
{
    public BlogNotFoundException(Guid Id)
        : base($"This Blog {Id} Not Found")
    {
    }
}