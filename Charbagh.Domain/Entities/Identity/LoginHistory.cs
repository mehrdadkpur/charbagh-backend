using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Identity;

public sealed class LoginHistory : BaseEntity
{
    public Guid? UserId { get; private set; }
    public string AttemptedMobile { get; private set; } = string.Empty;
    public DateTime LoginAt { get; private set; }
    public DateTime? LogoutAt { get; private set; }
    public bool IsSuccessful { get; private set; }
    public string? FailureReason { get; private set; }
    public string? IpAddress { get; private set; }
    public string? UserAgent { get; private set; }
    public string? DeviceName { get; private set; }
    public User? User { get; private set; }

    private LoginHistory() { }

    private LoginHistory(
        Guid? userId, string attemptedMobile, bool isSuccessful,
        string? failureReason, string? ipAddress, string? userAgent, string? deviceName)
    {
        Id = Guid.NewGuid();
        UserId = userId;
        AttemptedMobile = Limit(attemptedMobile, 20)!;
        LoginAt = DateTime.UtcNow;
        IsSuccessful = isSuccessful;
        FailureReason = Limit(failureReason, 500);
        IpAddress = Limit(ipAddress, 45);
        UserAgent = Limit(userAgent, 500);
        DeviceName = Limit(deviceName, 200);
    }

    public static LoginHistory Create(
        Guid? userId, string attemptedMobile, bool isSuccessful,
        string? failureReason = null, string? ipAddress = null,
        string? userAgent = null, string? deviceName = null)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("User id cannot be an empty GUID.", nameof(userId));
        if (string.IsNullOrWhiteSpace(attemptedMobile))
            throw new ArgumentException("Attempted mobile is required.", nameof(attemptedMobile));
        if (isSuccessful && !string.IsNullOrWhiteSpace(failureReason))
            throw new ArgumentException("Failure reason cannot be specified for a successful login.", nameof(failureReason));

        var history = new LoginHistory(userId, attemptedMobile, isSuccessful,
            failureReason, ipAddress, userAgent, deviceName);
        history.MarkAsCreated();
        return history;
    }

    private static string? Limit(string? value, int maxLength)
    {
        if (string.IsNullOrWhiteSpace(value)) return null;
        var normalized = value.Trim();
        return normalized.Length <= maxLength ? normalized : normalized[..maxLength];
    }

    public void RecordLogout()
    {
        if (!IsSuccessful || LogoutAt.HasValue) return;
        LogoutAt = DateTime.UtcNow;
        MarkAsUpdated();
    }
}
