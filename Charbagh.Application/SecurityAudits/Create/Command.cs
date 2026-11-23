using Charbagh.Application.SecurityAudits.Common;
using Charbagh.Domain.Enums;
using MediatR;

namespace Charbagh.Application.SecurityAudits.Create;

public sealed record CreateSecurityAuditCommand(
    Guid? UserId,
    SecurityAuditAction Action,
    bool Success,
    string? Description,
    string? IpAddress,
    string? UserAgent,
    string? DeviceName,
    string? Metadata
) : IRequest<SecurityAuditDto>;