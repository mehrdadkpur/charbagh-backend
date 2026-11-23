namespace Charbagh.Application.Common.Exceptions;

public sealed class UserAlreadyStudentException : Exception
{
    public UserAlreadyStudentException(Guid? UserId)
        : base($"The User With Code '{UserId}' already has been Student.")
    {
    }
}