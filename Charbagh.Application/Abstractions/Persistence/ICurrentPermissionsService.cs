using Charbagh.Application.Permissions.Current;

namespace Charbagh.Application.Common.Interfaces;

public interface ICurrentPermissionsService
{
    Task<CurrentPermissionsDto?> GetCurrentAsync( CancellationToken cancellationToken);
}