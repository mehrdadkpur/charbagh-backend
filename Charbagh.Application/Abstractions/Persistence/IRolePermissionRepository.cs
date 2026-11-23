using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IRolePermissionRepository
{
    Task<bool> ExistsAsync(
        Guid roleId,
        Guid permissionId,
        CancellationToken cancellationToken);

    Task AddAsync(
        RolePermission rolePermission,
        CancellationToken cancellationToken);

    Task RemoveAsync(
        Guid roleId,
        Guid permissionId,
        CancellationToken cancellationToken);
}