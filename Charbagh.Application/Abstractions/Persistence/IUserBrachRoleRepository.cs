
using Charbagh.Domain.Entities.CMS;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IUserBranchRoleRepository
    {
        Task AddAsync(UserBranchRole userBranchRole, CancellationToken cancellationToken);

        Task<UserBranchRole?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<UserBranchRole>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<UserBranchRole>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken);

        Task<IReadOnlyList<UserBranchRole>> GetByUserBranchIdAsync(Guid userBranchId, CancellationToken cancellationToken);

        Task<IReadOnlyList<UserBranchRole>> GetByIsActiveAsync(bool isActive, CancellationToken cancellationToken);

        Task<bool> ExistsAsync( Guid userBranchId,Guid roleId,CancellationToken cancellationToken);


    }
}
