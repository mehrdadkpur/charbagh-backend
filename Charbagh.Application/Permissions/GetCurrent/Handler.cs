using Charbagh.Application.Common.Interfaces;
using Charbagh.Application.Permissions.Current;
using MediatR;

namespace Charbagh.Application.Permissions.GetCurrent;

public sealed class GetCurrentPermissionsQueryHandler : IRequestHandler<GetCurrentPermissionsQuery, CurrentPermissionsDto?>
{
    private readonly ICurrentPermissionsService _permissionsService;

    public GetCurrentPermissionsQueryHandler( ICurrentPermissionsService permissionsService)
    {
        _permissionsService = permissionsService;
    }

    public Task<CurrentPermissionsDto?> Handle( GetCurrentPermissionsQuery request,CancellationToken cancellationToken)
    {
        return _permissionsService.GetCurrentAsync(cancellationToken);
    }
}