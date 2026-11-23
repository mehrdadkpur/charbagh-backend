
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class CommentRepository : ICommentRepository
    {
        private readonly CharbaghDbContext _context;

        public CommentRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(Comment comment, CancellationToken cancellationToken)
        {
            await _context.Comments.AddAsync(comment , cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Comments.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByBlogIdAsync(Guid BlogId, CancellationToken cancellationToken)
        {
            return await _context.Comments.AsNoTracking().Where(x => x.BlogId == BlogId).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<Comment?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Comments.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<IReadOnlyList<Comment>> GetByUserIdAsync(Guid UserId, CancellationToken cancellationToken)
        {
            return await _context.Comments.AsNoTracking().Where(x => x.UserId == UserId).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }
    }
}
