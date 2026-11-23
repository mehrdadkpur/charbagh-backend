using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IPermissionRepository
    {
        Task AddAsync(Permission permission, CancellationToken cancellationToken);

        Task<Permission?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<bool> ExistsAsync(string module, PermissionAction action, CancellationToken cancellationToken);

        Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<Permission>> GetByModuleAsync(string ModuleName,CancellationToken cancellationToken);

        Task<IReadOnlyList<Permission>> GetByPermissionNameAsync(string PermissionName,CancellationToken cancellationToken);

        Task<IReadOnlyList<Permission>> GetByPermissionActionAsync(PermissionAction PermissionAction,CancellationToken cancellationToken);

        Task<IReadOnlyList<Permission>> GetByIsSystemPermissionAsync(bool IsSystemPermission, CancellationToken cancellationToken);

    }
}
