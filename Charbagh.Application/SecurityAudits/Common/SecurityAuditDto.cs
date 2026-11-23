using Charbagh.Domain.Enums;

namespace Charbagh.Application.SecurityAudits.Common;

public sealed record SecurityAuditDto(
    Guid Id,
    Guid? UserId,
    SecurityAuditAction Action,
    bool Success,
    string? Description,
    string? IpAddress,
    string? UserAgent,
    string? DeviceName,
    string? Metadata,
    DateTime CreatedAt);