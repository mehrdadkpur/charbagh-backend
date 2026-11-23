namespace Charbagh.Application.Common.Exceptions;

public sealed class UserAlreadyParentException : Exception
{
    public UserAlreadyParentException(Guid? UserId)
        : base($"The User With Code '{UserId}' already has been Teacher.")
    {
    }
}