namespace Charbagh.Application.RefreshTokens.Common
{
    public sealed record RefreshTokenDto(
        Guid Id,
        Guid UserId,
        string Token,
        DateTime ExpiresAt,
        DateTime? RevokedAt,
        string? ReplacedByToken,
        string? IpAddress,
        string? UserAgent,
        string? DeviceName
        );
}
