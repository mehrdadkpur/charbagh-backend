using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Permissions.Common;
using MediatR;

namespace Charbagh.Application.Permissions.GetById
{
    public sealed class GetPermissionByIdHandler : IRequestHandler<GetPermissionByIdQuery , PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;


        public GetPermissionByIdHandler(IPermissionRepository permissionRepository)
        {
            _permissionRepository = permissionRepository;
        }

        public async Task<PermissionDto> Handle(GetPermissionByIdQuery request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (permission == null)
            {
                throw new PermissionNotFoundException(request.Id);
            }

            return permission.ToDto();
        }
    }
}
