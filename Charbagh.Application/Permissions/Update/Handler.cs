using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Permissions.Common;
using Charbagh.Application.Permissions.Create;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Permissions.Update
{
    public sealed class UpdatePermissionHandler : IRequestHandler<UpdatePermissionCommand, PermissionDto>
    {
        private readonly IPermissionRepository _permissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePermissionHandler(IPermissionRepository permissionRepository, IUnitOfWork unitOfWork)
        {
            _permissionRepository = permissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<PermissionDto> Handle(UpdatePermissionCommand request, CancellationToken cancellationToken)
        {
            var permission = await _permissionRepository.GetByIdAsync(request.Id, cancellationToken);

            if (permission == null)
            {
                throw new PermissionNotFoundException(request.Id);
            }

            var permissionExists = await _permissionRepository.ExistsAsync(request.Module, request.Action, cancellationToken);

            if (permissionExists)
            {
                throw new DuplicatePermissionException(request.Module, request.Action);
            }

            permission.Update(request.Module, request.Name, request.Action, request.DisplayName, request.Description);

            await _permissionRepository.AddAsync(permission, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return permission.ToDto();
        }
    }
}
