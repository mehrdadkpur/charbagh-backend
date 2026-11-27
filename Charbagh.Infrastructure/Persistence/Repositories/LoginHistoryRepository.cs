using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class LoginHistoryRepository
    : ILoginHistoryRepository
{
    private readonly CharbaghDbContext _context;

    public LoginHistoryRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        LoginHistory loginHistory,
        CancellationToken cancellationToken)
    {
        await _context.LoginHistories.AddAsync(
            loginHistory,
            cancellationToken);
    }

    public async Task<LoginHistory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.LoginHistories
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<LoginHistory>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.LoginHistories
            .AsNoTracking()
            .Include(x => x.User)
            .OrderByDescending(x => x.LoginAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<LoginHistory>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.LoginHistories
            .AsNoTracking()
            .Include(x => x.User)
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.LoginAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<LoginHistory?> GetForUpdateAsync(
    Guid id,
    CancellationToken cancellationToken)
    {
        return await _context.LoginHistories
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }
}