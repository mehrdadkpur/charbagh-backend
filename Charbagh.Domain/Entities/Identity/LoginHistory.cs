using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Identity;

public sealed class LoginHistory : BaseEntity
{
    public Guid UserId { get; private set; }

    public DateTime LoginAt { get; private set; }

    public DateTime? LogoutAt { get; private set; }

    public bool IsSuccessful { get; private set; }

    public string? FailureReason { get; private set; }

    public string? IpAddress { get; private set; }

    public string? UserAgent { get; private set; }

    public string? DeviceName { get; private set; }

    public User User { get; private set; } = null!;

    private LoginHistory()
    {
    }

    private LoginHistory(
        Guid userId,
        bool isSuccessful,
        string? failureReason,
        string? ipAddress,
        string? userAgent,
        string? deviceName)
    {
        UserId = userId;
        LoginAt = DateTime.UtcNow;
        IsSuccessful = isSuccessful;
        FailureReason = failureReason?.Trim();
        IpAddress = ipAddress?.Trim();
        UserAgent = userAgent?.Trim();
        DeviceName = deviceName?.Trim();
    }

    public static LoginHistory Create(
        Guid userId,
        bool isSuccessful,
        string? failureReason = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? deviceName = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(
                "User id is required.",
                nameof(userId));

        if (isSuccessful && !string.IsNullOrWhiteSpace(failureReason))
            throw new ArgumentException(
                "Failure reason cannot be specified for a successful login.",
                nameof(failureReason));

        var loginHistory = new LoginHistory(
            userId,
            isSuccessful,
            failureReason,
            ipAddress,
            userAgent,
            deviceName);

        loginHistory.MarkAsCreated();

        return loginHistory;
    }

    public void RecordLogout()
    {
        if (LogoutAt.HasValue)
            return;

        LogoutAt = DateTime.UtcNow;

        MarkAsUpdated();
    }
}