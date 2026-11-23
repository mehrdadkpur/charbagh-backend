using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetByAction;

public sealed class GetSecurityAuditsByActionHandler: IRequestHandler<GetSecurityAuditsByActionQuery,IReadOnlyList<SecurityAuditDto>>
{
    private readonly ISecurityAuditRepository _securityAuditRepository;

    public GetSecurityAuditsByActionHandler( ISecurityAuditRepository securityAuditRepository)
    {
        _securityAuditRepository = securityAuditRepository;
    }

    public async Task<IReadOnlyList<SecurityAuditDto>> Handle( GetSecurityAuditsByActionQuery request, CancellationToken cancellationToken)
    {
        var audits = await _securityAuditRepository.GetByActionAsync( request.Action, cancellationToken);

        return audits.Select(x => x.ToDto()).ToList();
    }
}