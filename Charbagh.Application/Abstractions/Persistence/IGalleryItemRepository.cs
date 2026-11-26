
using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IGalleryItemRepository
    {
        Task AddAsync(GalleryItem galleryItem, CancellationToken cancellationToken);

        Task<GalleryItem?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<GalleryItem>> GetAllAsync(CancellationToken cancellationToken);

        void Remove(GalleryItem galleryItem);

    }
}
