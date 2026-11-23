using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Branches;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class BranchRepository : IBranchRepository
{
    private readonly CharbaghDbContext _context;

    public BranchRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync( Branch branch, CancellationToken cancellationToken)
    {
        await _context.Branches.AddAsync(branch, cancellationToken);
    }

    public async Task<Branch?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return await _context.Branches
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<Branch>> GetAllAsync( CancellationToken cancellationToken)
    {
        return await _context.Branches
            .AsNoTracking()
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<Branch?> GetByCodeAsync( string code,CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(code))
            return null;

        return await _context.Branches
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Code == code,
                cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Branches.CountAsync(cancellationToken);
    }
}