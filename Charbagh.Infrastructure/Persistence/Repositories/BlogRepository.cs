
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class BlogRepository : IBlogRepository
    {
        private readonly CharbaghDbContext _context;

        public BlogRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }
        public async Task AddAsync(Blog blog, CancellationToken cancellationToken)
        {
           await _context.Blogs.AddAsync(blog , cancellationToken);
        }

        public async Task<IReadOnlyList<Blog>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Blogs
                .AsNoTracking()
                .Include(x=> x.Author)
                .OrderByDescending(x => x.CreatedAt)
                .ToListAsync(cancellationToken);
        }

        public async Task<Blog?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Blogs
                .Include(x => x.Author)
                .FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<IReadOnlyList< Blog>> GetByUserIdAsync(Guid UserId, CancellationToken cancellationToken)
        {
            return await _context.Blogs.AsNoTracking().OrderByDescending(x => x.UserId).ToListAsync(cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Blogs.CountAsync(cancellationToken);
        }
    }
}
