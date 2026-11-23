using Charbagh.Domain.Enums;

namespace Charbagh.Domain.Entities.Identity;

public sealed class SecurityAudit : BaseEntity
{
    public Guid? UserId { get; private set; }

    public SecurityAuditAction Action { get; private set; }

    public bool Success { get; private set; }

    public string? Description { get; private set; }

    public string? IpAddress { get; private set; }

    public string? UserAgent { get; private set; }

    public string? DeviceName { get; private set; }

    public string? Metadata { get; private set; }

    public User? User { get; private set; }

    private SecurityAudit()
    {
    }

    private SecurityAudit(
        Guid? userId,
        SecurityAuditAction action,
        bool success,
        string? description,
        string? ipAddress,
        string? userAgent,
        string? deviceName,
        string? metadata)
    {
        UserId = userId;
        Action = action;
        Success = success;
        Description = description;
        IpAddress = ipAddress;
        UserAgent = userAgent;
        DeviceName = deviceName;
        Metadata = metadata;
    }

    public static SecurityAudit Create(
        Guid? userId,
        SecurityAuditAction action,
        bool success,
        string? description = null,
        string? ipAddress = null,
        string? userAgent = null,
        string? deviceName = null,
        string? metadata = null)
    {
        if (userId.HasValue && userId.Value == Guid.Empty)
            throw new ArgumentException(
                "User id is invalid.",
                nameof(userId));

        var securityAudit = new SecurityAudit(
            userId,
            action,
            success,
            description?.Trim(),
            ipAddress?.Trim(),
            userAgent?.Trim(),
            deviceName?.Trim(),
            metadata);

        securityAudit.MarkAsCreated();

        return securityAudit;
    }
}
