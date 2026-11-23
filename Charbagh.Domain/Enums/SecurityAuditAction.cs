namespace Charbagh.Domain.Enums
{
    public enum SecurityAuditAction
    {
        Login = 1,
        LoginFailed = 2,

        Logout = 3,

        RefreshToken = 4,
        RefreshTokenFailed = 5,

        PasswordChanged = 6,
        PasswordChangeFailed = 7,

        PasswordResetRequested = 8,
        PasswordReset = 9,
        PasswordResetFailed = 10,

        MobileConfirmed = 11,
        EmailConfirmed = 12,

        AccountLocked = 13,
        AccountUnlocked = 14,

        AccountSuspended = 15,
        AccountActivated = 16,

        SecurityStampRegenerated = 17
    }
}
