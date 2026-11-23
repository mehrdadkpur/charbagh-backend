using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetAll;

public sealed class GetAllSecurityAuditsHandler : IRequestHandler< GetAllSecurityAuditsQuery,IReadOnlyList<SecurityAuditDto>>
{
    private readonly ISecurityAuditRepository _securityAuditRepository;

    public GetAllSecurityAuditsHandler(ISecurityAuditRepository securityAuditRepository)
    {
        _securityAuditRepository = securityAuditRepository;
    }

    public async Task<IReadOnlyList<SecurityAuditDto>> Handle( GetAllSecurityAuditsQuery request,CancellationToken cancellationToken)
    {
        var audits = await _securityAuditRepository.GetAllAsync( cancellationToken);

        return audits.Select(x => x.ToDto()) .ToList();
    }
}