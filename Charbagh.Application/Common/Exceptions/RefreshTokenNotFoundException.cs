namespace Charbagh.Application.Common.Exceptions;

public sealed class RefreshTokenNotFoundException : Exception
{
    public RefreshTokenNotFoundException(Guid Id)
        : base($"This Refresh Token {Id} Not Found")
    {
    }
}