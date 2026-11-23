namespace Charbagh.Application.Common.Exceptions;

public sealed class RefreshTokenNotfound : Exception
{
    public RefreshTokenNotfound(Guid Id)
        : base($"This Refresh Token '{Id}' Not Found")
    {
    }
}