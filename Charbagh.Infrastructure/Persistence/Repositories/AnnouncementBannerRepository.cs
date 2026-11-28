using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class AnnouncementBannerRepository : IAnnouncementBannerRepository
{
    private readonly CharbaghDbContext _context;

    public AnnouncementBannerRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(AnnouncementBanner banner, CancellationToken cancellationToken)
    {
        await _context.AnnouncementBanners.AddAsync(banner, cancellationToken);
    }

    public async Task<AnnouncementBanner?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
    {
        return await _context.AnnouncementBanners
            .FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
    }

    public async Task<IReadOnlyList<AnnouncementBanner>> GetAllAsync(CancellationToken cancellationToken)
    {
        return await _context.AnnouncementBanners
            .AsNoTracking()
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<AnnouncementBanner>> GetActiveAsync(
        DateTime utcNow,
        CancellationToken cancellationToken)
    {
        return await _context.AnnouncementBanners
            .AsNoTracking()
            .Where(x => x.Status == BannerStatus.Published
                && (x.StartsAt == null || x.StartsAt <= utcNow)
                && (x.EndsAt == null || x.EndsAt > utcNow))
            .OrderBy(x => x.SortOrder)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}
