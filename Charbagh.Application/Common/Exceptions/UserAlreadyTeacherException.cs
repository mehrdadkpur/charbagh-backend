namespace Charbagh.Application.Common.Exceptions;

public sealed class UserAlreadyTeacherException : Exception
{
    public UserAlreadyTeacherException(Guid? UserId)
        : base($"The User With Code '{UserId}' already has been Teacher.")
    {
    }
}