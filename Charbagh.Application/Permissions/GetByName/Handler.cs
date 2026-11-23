using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetByName
{
    public sealed class GetPermissionsByNameHandler : IRequestHandler<GetPermissionsByNameQuery, IReadOnlyList<PermissionDto>>
    {
        private readonly IPermissionRepository _permissionRepository;


        public GetPermissionsByNameHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<IReadOnlyList<PermissionDto>> Handle(GetPermissionsByNameQuery request, CancellationToken cancellationToken)
        {
            var permissions = await _permissionRepository.GetByModuleAsync(request.Name, cancellationToken);

            return permissions.Select(x => x.ToDto()).ToList();
        }
    }

}
