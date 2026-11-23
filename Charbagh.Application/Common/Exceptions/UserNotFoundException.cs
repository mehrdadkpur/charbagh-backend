namespace Charbagh.Application.Common.Exceptions;

public sealed class UserNotFoundException : Exception
{
    public UserNotFoundException(Guid id)
        : base("This User Not Found")
    {
    }
}