namespace Charbagh.Domain.Entities.Identity;

public sealed class RefreshToken : BaseEntity
{
    public Guid UserId { get; private set; }

    public Guid? LoginHistoryId { get; private set; }

    public string Token { get; private set; } = null!;

    public DateTime ExpiresAt { get; private set; }

    public DateTime? RevokedAt { get; private set; }

    public string? ReplacedByToken { get; private set; }

    public string? IpAddress { get; private set; }

    public string? UserAgent { get; private set; }

    public string? DeviceName { get; private set; }

    public User User { get; private set; } = null!;

    public bool IsExpired => DateTime.UtcNow >= ExpiresAt;

    public bool IsRevoked => RevokedAt.HasValue;

    public bool IsActive => !IsExpired && !IsRevoked;

    private RefreshToken()
    {
    }

    private RefreshToken(
        Guid userId,
        string token,
        DateTime expiresAt,
        string? ipAddress,
        string? userAgent,
        string? deviceName,
        Guid? loginHistoryId)
    {
        UserId = userId;
        Token = token;
        ExpiresAt = expiresAt;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        DeviceName = deviceName;
        LoginHistoryId = loginHistoryId;
    }

    public static RefreshToken Create(
        Guid userId,
        string token,
        DateTime expiresAt,
        string? ipAddress = null,
        string? userAgent = null,
        string? deviceName = null,
        Guid? loginHistoryId = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(
                "User id is required.",
                nameof(userId));

        if (string.IsNullOrWhiteSpace(token))
            throw new ArgumentException(
                "Refresh token is required.",
                nameof(token));

        if (expiresAt <= DateTime.UtcNow)
            throw new ArgumentException(
                "Refresh token expiration must be in the future.",
                nameof(expiresAt));

        var refreshToken = new RefreshToken(
            userId,
            token.Trim(),
            expiresAt,
            ipAddress?.Trim(),
            userAgent?.Trim(),
            deviceName?.Trim(),
            loginHistoryId);

        refreshToken.MarkAsCreated();

        return refreshToken;
    }

    public void Revoke()
    {
        if (IsRevoked)
            return;

        RevokedAt = DateTime.UtcNow;

        MarkAsUpdated();
    }

    public void Replace(string replacedByToken)
    {
        if (string.IsNullOrWhiteSpace(replacedByToken))
            throw new ArgumentException(
                "Replacement token is required.",
                nameof(replacedByToken));

        if (IsRevoked)
            throw new InvalidOperationException(
                "A revoked refresh token cannot be replaced.");

        ReplacedByToken = replacedByToken.Trim();
        RevokedAt = DateTime.UtcNow;

        MarkAsUpdated();
    }
}
