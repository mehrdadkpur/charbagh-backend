
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class GalleryItemRepository : IGalleryItemRepository
    {
        private readonly CharbaghDbContext _context;

        public GalleryItemRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(GalleryItem galleryItem, CancellationToken cancellationToken)
        {
            await _context.GalleryItems.AddAsync(galleryItem , cancellationToken) ;
        }

        public async Task<IReadOnlyList<GalleryItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.GalleryItems.AsNoTracking().ToListAsync(cancellationToken) ;
        }

        public async Task<GalleryItem?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.GalleryItems.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public void Remove(GalleryItem galleryItem)
        {
            _context.GalleryItems.Remove(galleryItem);
        }

    }
}
