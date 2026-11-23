
using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IBookItemRepository
    {
        Task AddAsync (BookItem bookItem , CancellationToken cancellationToken);

        Task<BookItem?> GetByIdAsync (Guid Id , CancellationToken cancellationToken);

        Task<IReadOnlyList<BookItem>> GetAllAsync (CancellationToken cancellationToken);

        Task <BookItem?> GetByNameAsync (string Name , CancellationToken cancellationToken);

        Task <bool> ExistsBookAsync (Guid BookId , CancellationToken cancellationToken);

        Task<bool> ExistsByBookItemNameAsync ( string Title , CancellationToken cancellationToken);
    }
}
