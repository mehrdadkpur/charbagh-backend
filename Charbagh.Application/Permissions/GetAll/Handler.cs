using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetAll
{
    public sealed class GetAllPermissionsHandler : IRequestHandler<GetAllPermissionsQuery,IReadOnlyList< PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;


        public GetAllPermissionsHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IReadOnlyList< PermissionDto>> Handle(GetAllPermissionsQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetAllAsync(cancellationToken);

            return permissions.Select(x => x.ToDto()).ToList();
        }
    }
}
