namespace Charbagh.Application.Abstractions.Security;

public interface IPermissionChecker
{
    Task<bool> HasPermissionAsync(
        Guid userId,
        Guid branchId,
        string module,
        PermissionAction action,
        CancellationToken cancellationToken);
}