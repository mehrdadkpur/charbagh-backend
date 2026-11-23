using Charbagh.Domain.Entities.Branches;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IBranchRepository
    {
        Task AddAsync(Branch branch, CancellationToken cancellationToken);

        Task<Branch?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task <IReadOnlyList<Branch>> GetAllAsync(CancellationToken cancellationToken);

        Task<Branch?> GetByCodeAsync( string code,CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
