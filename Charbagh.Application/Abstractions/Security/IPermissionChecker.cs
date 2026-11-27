namespace Charbagh.Application.Abstractions.Security;

public interface IPermissionChecker
{
    Task<bool> HasAnyActiveRoleAsync(
        Guid userId,
        Guid branchId,
        CancellationToken cancellationToken);

    Task<bool> HasPermissionAsync(
        Guid userId,
        Guid branchId,
        string module,
        PermissionAction action,
        CancellationToken cancellationToken);
}