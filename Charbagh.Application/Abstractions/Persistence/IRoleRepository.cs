using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IRoleRepository
    {
        Task AddAsync(Role role, CancellationToken cancellationToken);

        Task<Role?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<bool> ExistsNameAsync(string name, Guid? excludeRoleId , CancellationToken cancellationToken);

        Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<Role>> GetByNameAsync(string roleName, CancellationToken cancellationToken);

        Task<IReadOnlyList<Role>> GetByIsSystemAsync(bool IsSystemRole, CancellationToken cancellationToken);

        Task<IReadOnlyList<Role>> GetByIsActiveAsync(bool IsSystemRole, CancellationToken cancellationToken);

        Task<bool> HasPermissionAsync( Guid roleId, Guid permissionId, CancellationToken cancellationToken);

    }
}
