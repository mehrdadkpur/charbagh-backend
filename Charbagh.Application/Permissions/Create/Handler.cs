using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Permissions.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Permissions.Create
{
    public sealed class CreatePermissionHandler : IRequestHandler<CreatePermissionCommand,PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreatePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissionDto> Handle(CreatePermissionCommand request , CancellationToken cancellationToken)
        {
            var permissionExists = await _permissionRepository.ExistsAsync(request.Module, request.Action, cancellationToken);

            if (permissionExists)
            {
                throw new DuplicatePermissionException(request.Module, request.Action);
            }

            var permission = Permission.Create(request.Module, request.Name, request.Action, request.DisplayName, request.Description, request.IsSystem);

            await _permissionRepository.AddAsync(permission , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return permission.ToDto();
        }
    }
}
