using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IBlogRepository
    {
        Task AddAsync(Blog blog, CancellationToken cancellationToken);

        Task<Blog?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Blog>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList< Blog>> GetByUserIdAsync(Guid UserId, CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
