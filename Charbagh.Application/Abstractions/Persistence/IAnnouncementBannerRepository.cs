using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IAnnouncementBannerRepository
{
    Task AddAsync(AnnouncementBanner banner, CancellationToken cancellationToken);
    Task<AnnouncementBanner?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<IReadOnlyList<AnnouncementBanner>> GetAllAsync(CancellationToken cancellationToken);
    Task<IReadOnlyList<AnnouncementBanner>> GetActiveAsync(DateTime utcNow, CancellationToken cancellationToken);
}
