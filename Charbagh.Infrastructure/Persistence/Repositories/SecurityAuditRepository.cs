using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Domain.Enums;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories.Identity;

public sealed class SecurityAuditRepository : ISecurityAuditRepository
{
    private readonly CharbaghDbContext _context;

    public SecurityAuditRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        SecurityAudit securityAudit,
        CancellationToken cancellationToken)
    {
        await _context.SecurityAudits.AddAsync(
            securityAudit,
            cancellationToken);
    }

    public async Task<SecurityAudit?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.SecurityAudits
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<SecurityAudit>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.SecurityAudits
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SecurityAudit>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.SecurityAudits
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<SecurityAudit>> GetByActionAsync(
        SecurityAuditAction action,
        CancellationToken cancellationToken)
    {
        return await _context.SecurityAudits
            .AsNoTracking()
            .Where(x => x.Action == action)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}