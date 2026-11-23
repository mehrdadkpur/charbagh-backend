namespace Charbagh.Application.Common.Exceptions;

public sealed class UserAlreadyStaffException : Exception
{
    public UserAlreadyStaffException(Guid? UserId)
        : base($"The User With Code '{UserId}' already has been Staff.")
    {
    }
}