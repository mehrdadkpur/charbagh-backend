using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.SecurityAudits.Common;

public static class SecurityAuditMapper
{
    public static SecurityAuditDto ToDto(
        this SecurityAudit securityAudit)
    {
        return new SecurityAuditDto(
            securityAudit.Id,
            securityAudit.UserId,
            securityAudit.Action,
            securityAudit.Success,
            securityAudit.Description,
            securityAudit.IpAddress,
            securityAudit.UserAgent,
            securityAudit.DeviceName,
            securityAudit.Metadata,
            securityAudit.CreatedAt);
    }
}