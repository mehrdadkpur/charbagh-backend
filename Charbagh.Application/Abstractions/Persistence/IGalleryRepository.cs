using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IGalleryRepository
    {
        Task AddAsync(Gallery gallery, CancellationToken cancellationToken);

        Task<Gallery?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Gallery>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList< Gallery>> GetByGalleryTitleAsync(string Title, CancellationToken cancellationToken);

        Task<IReadOnlyList< Gallery>> GetByGalleryStatusAsync(GalleryStatus Status, CancellationToken cancellationToken);
    }
}
