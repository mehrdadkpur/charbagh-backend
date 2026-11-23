
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class BookItemRepository : IBookItemRepository
    {
        private readonly CharbaghDbContext _context;

        public BookItemRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(BookItem bookItem, CancellationToken cancellationToken)
        {
            await _context.AddAsync(bookItem, cancellationToken);
        }

        public async Task<bool> ExistsBookAsync(Guid BookId, CancellationToken cancellationToken)
        {
            return await _context.BookItems.AnyAsync(x => x.BookId == BookId);
        }

        public async Task<bool> ExistsByBookItemNameAsync(string BookItemTitle, CancellationToken cancellationToken)
        {
           return await _context.BookItems.AnyAsync(x => x.Title == BookItemTitle , cancellationToken);
        }

        public async Task<IReadOnlyList<BookItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.BookItems.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<BookItem?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.BookItems.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<BookItem?> GetByNameAsync(string BookName, CancellationToken cancellationToken)
        {
           return await _context.BookItems.FirstOrDefaultAsync(x => x.Title == BookName, cancellationToken);
        }
    }
}
