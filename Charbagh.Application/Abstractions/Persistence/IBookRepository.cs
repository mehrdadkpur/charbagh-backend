
using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IBookRepository
    {
        Task AddAsync (Book book , CancellationToken cancellationToken);

        Task <Book?> GetByIdAsync (Guid bookId , CancellationToken cancellationToken);

        Task<IReadOnlyList<Book>> GetAllAsync (CancellationToken cancellationToken);

        Task<bool> ExistsByBookNameAsync (string bookName, Guid? excludeBookId , CancellationToken cancellationToken);

        Task<bool> ExistsByInstrumentIdAsync(Guid instrumentId , CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
