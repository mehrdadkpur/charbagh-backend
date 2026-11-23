using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface INationalityRepository
    {
        Task AddAsync(Nationality nationality, CancellationToken cancellationToken);

        Task<Nationality?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Nationality>> GetAllAsync( CancellationToken cancellationToken);

        Task<bool> ExistsByCodeAsync(string code , CancellationToken cancellationToken);

        Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken);
    }
}
