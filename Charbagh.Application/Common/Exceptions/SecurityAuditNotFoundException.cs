namespace Charbagh.Application.Common.Exceptions;

public sealed class SecurityAuditNotFoundException : Exception
{
    public SecurityAuditNotFoundException(Guid Id)
        : base($"This Security Audit {Id} Not Found")
    {
    }
}