using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByIsSystem
{
    public sealed class GetPermissionsByIsSystemHandler : IRequestHandler<GetPermissionsByIsSystemQuery, IReadOnlyList<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;


        public GetPermissionsByIsSystemHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IReadOnlyList<PermissionDto>> Handle(GetPermissionsByIsSystemQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetByIsSystemPermissionAsync(request.IsSystem,cancellationToken);

            return permissions.Select(x => x.ToDto()).ToList();
        }
    }
}
