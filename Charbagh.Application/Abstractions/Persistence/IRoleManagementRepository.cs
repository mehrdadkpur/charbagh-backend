using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IRoleManagementRepository
{
    Task<Role?> GetDetailsAsync(
        Guid roleId,
        CancellationToken cancellationToken);

    Task<UserBranch?> GetUserBranchAsync(
        Guid userBranchId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<UserBranch>> GetAvailableUsersAsync(
        Guid roleId,
        Guid branchId,
        CancellationToken cancellationToken);

    Task<UserBranchRole?> GetUserBranchRoleAsync(
        Guid roleId,
        Guid userBranchId,
        CancellationToken cancellationToken);

    Task AddUserBranchRoleAsync(
        UserBranchRole userBranchRole,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Permission>> GetPermissionsAsync(
        CancellationToken cancellationToken);
}