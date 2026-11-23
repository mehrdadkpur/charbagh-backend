
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IUserBranchRepository
    {
        Task AddAsync (UserBranch userBranch , CancellationToken cancellationToken);
        Task <UserBranch?> GetByIdAsync(Guid id , CancellationToken cancellationToken);
        Task <UserBranch?> GetDefaultAsync(Guid userId , Guid branchId , CancellationToken cancellationToken);
        Task <UserBranch?> GetDefaultByUserIdAsync(Guid userId , CancellationToken cancellationToken);
        Task<IReadOnlyList<UserBranch>> GetAllAsync (CancellationToken cancellationToken);
        Task<IReadOnlyList< UserBranch>> GetByUserIdAsync (Guid userId , CancellationToken cancellationToken);
        Task<IReadOnlyList<UserBranch>> GetByBranchIdAsync (Guid branchId , CancellationToken cancellationToken);
        Task<IReadOnlyList<UserBranch>> GetByAssigner (Guid assignerId , CancellationToken cancellationToken);
        Task<bool> ExistsAsync (Guid userId , Guid branchId , CancellationToken cancellationToken);
        Task<bool> ExistsIsDefaultAsync (CancellationToken cancellationToken);
    }
}
