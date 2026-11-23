using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class BookRepository : IBookRepository
    {
        private readonly CharbaghDbContext _context;

        public BookRepository(CharbaghDbContext bookConext)
        {
            _context = bookConext;
        }
        public async Task AddAsync(Book book, CancellationToken cancellationToken)
        {
            await _context.Books.AddAsync(book , cancellationToken);
        }

        public async Task<bool> ExistsByBookNameAsync(string bookName, Guid? excludeBookId , CancellationToken cancellationToken)
        {
            return await _context.Books.AnyAsync(x => x.Title == bookName && (!excludeBookId.HasValue || x.Id != excludeBookId.Value) , cancellationToken);
        }

        public async Task<bool> ExistsByInstrumentIdAsync(Guid instrumentId, CancellationToken cancellationToken)
        {
            return await _context.Books.AnyAsync(x =>x.InstrumentId == instrumentId , cancellationToken);
        }

        public async Task<IReadOnlyList<Book>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Books.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Book?> GetByIdAsync(Guid bookId, CancellationToken cancellationToken)
        {
           return await _context.Books.FirstOrDefaultAsync(x => x.Id == bookId);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Books.CountAsync(cancellationToken);
        }
    }
}
