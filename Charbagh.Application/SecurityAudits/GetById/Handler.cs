using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetById;

public sealed class GetSecurityAuditByIdHandler : IRequestHandler<GetSecurityAuditByIdQuery, SecurityAuditDto>
{
    private readonly ISecurityAuditRepository _securityAuditRepository;

    public GetSecurityAuditByIdHandler(ISecurityAuditRepository securityAuditRepository)
    {
        _securityAuditRepository = securityAuditRepository;
    }

    public async Task<SecurityAuditDto> Handle( GetSecurityAuditByIdQuery request, CancellationToken cancellationToken)
    {
        var securityAudit = await _securityAuditRepository.GetByIdAsync( request.Id, cancellationToken);

        if (securityAudit is null)
            throw new SecurityAuditNotFoundException(request.Id);

        return securityAudit.ToDto();
    }
}