
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class GalleryRepository : IGalleryRepository
    {
        private readonly CharbaghDbContext _context;

        public GalleryRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(Gallery gallery, CancellationToken cancellationToken)
        {
            await _context.Galleries.AddAsync(gallery,cancellationToken);
        }

        public async Task<IReadOnlyList<Gallery>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Galleries.AsNoTracking().OrderByDescending(x=> x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Gallery>> GetByGalleryStatusAsync(GalleryStatus Status, CancellationToken cancellationToken)
        {
            return await _context.Galleries.AsNoTracking().Where(x => x.Status == Status).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Gallery>> GetByGalleryTitleAsync(string Title, CancellationToken cancellationToken)
        {
            return await _context.Galleries.AsNoTracking().Where(x => x.Title == Title).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<Gallery?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Galleries.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }
    }
}
