using Charbagh.Domain.Entities.Identity;
using Charbagh.Domain.Enums;

namespace Charbagh.Application.Abstractions.Persistence.Repositories.Identity;

public interface ISecurityAuditRepository
{
    Task AddAsync( SecurityAudit securityAudit,CancellationToken cancellationToken);

    Task<SecurityAudit?> GetByIdAsync(Guid id,CancellationToken cancellationToken);

    Task<IReadOnlyList<SecurityAudit>> GetAllAsync(CancellationToken cancellationToken);

    Task<IReadOnlyList<SecurityAudit>> GetByUserIdAsync( Guid userId, CancellationToken cancellationToken);

    Task<IReadOnlyList<SecurityAudit>> GetByActionAsync( SecurityAuditAction action, CancellationToken cancellationToken);
}