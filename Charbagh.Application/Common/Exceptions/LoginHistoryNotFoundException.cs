namespace Charbagh.Application.Common.Exceptions;

public sealed class LoginHistoryNotFoundException : Exception
{
    public LoginHistoryNotFoundException(Guid Id)
        : base($"This Login History {Id} Not Found")
    {
    }
}