using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.SecurityAudits.Common;
using MediatR;

namespace Charbagh.Application.SecurityAudits.GetByUserId;

public sealed class GetSecurityAuditsByUserIdHandler : IRequestHandler< GetSecurityAuditsByUserIdQuery,IReadOnlyList<SecurityAuditDto>>
{
    private readonly ISecurityAuditRepository _securityAuditRepository;

    public GetSecurityAuditsByUserIdHandler(ISecurityAuditRepository securityAuditRepository)
    {
        _securityAuditRepository = securityAuditRepository;
    }

    public async Task<IReadOnlyList<SecurityAuditDto>> Handle( GetSecurityAuditsByUserIdQuery request, CancellationToken cancellationToken)
    {
        if (request.UserId == Guid.Empty)
            throw new ArgumentException( "User id is required.", nameof(request.UserId));

        var audits = await _securityAuditRepository.GetByUserIdAsync( request.UserId, cancellationToken);

        return audits.Select(x => x.ToDto()).ToList();
    }
}