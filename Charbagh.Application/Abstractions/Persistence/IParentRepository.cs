using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IParentRepository
    {
        Task AddAsync(Parent parent, CancellationToken cancellationToken);
        Task<Parent?> GetByIdAsync(Guid parentId, CancellationToken cancellationToken);
        Task<Parent?> GetDetailsByIdAsync(Guid parentId, CancellationToken cancellationToken);
        Task<IReadOnlyList<Parent>> GetAllAsync(CancellationToken cancellationToken);
        Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken);
    }
}
