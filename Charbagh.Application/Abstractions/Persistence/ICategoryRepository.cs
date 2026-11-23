
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ICategoryRepository
    {
        Task AddAsync(Category category, CancellationToken cancellationToken);

        Task<Category?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken);

        Task<Category?> GetByNameAsync(string CategoryName, CancellationToken cancellationToken);
    }
}
