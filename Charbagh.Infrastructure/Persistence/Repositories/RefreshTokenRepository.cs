using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class RefreshTokenRepository : IRefreshTokenRepository
{
    private readonly CharbaghDbContext _context;

    public RefreshTokenRepository(CharbaghDbContext charbaghDbContext)
    {
        _context = charbaghDbContext;
    }

    public async Task AddAsync(
        RefreshToken refreshToken,
        CancellationToken cancellationToken)
    {
        await _context.RefreshTokens.AddAsync(
            refreshToken,
            cancellationToken);
    }

    public async Task<RefreshToken?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<RefreshToken?> GetByTokenAsync(
        string token,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Token == token,
                cancellationToken);
    }

    public async Task<RefreshToken?> GetForUpdateAsync(
        string token,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .FirstOrDefaultAsync(
                x => x.Token == token,
                cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetByUserIdForUpdateAsync(
        Guid userId,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .Where(x => x.UserId == userId)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<RefreshToken>> GetByIPAddressAsync(
        string ipAddress,
        CancellationToken cancellationToken)
    {
        return await _context.RefreshTokens
            .AsNoTracking()
            .Where(x => x.IpAddress == ipAddress)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

}