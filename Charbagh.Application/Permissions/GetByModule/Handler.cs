
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByModule
{
    public sealed class GetPermissionsByModuleHandler : IRequestHandler<GetPermissionsByModuleQuery, IReadOnlyList<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;


        public GetPermissionsByModuleHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IReadOnlyList<PermissionDto>> Handle(GetPermissionsByModuleQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetByModuleAsync(request.Module, cancellationToken);

            return permissions.Select(x => x.ToDto()).ToList();
        }
    }

}
