using Charbagh.Application.Abstractions.Persistence;
using MediatR;

namespace Charbagh.Application.RolePermissions.Remove
{
    public sealed class RemovePermissionFromRoleHandler : IRequestHandler<RemovePermissionFromRoleCommand>
    {
        private readonly IRolePermissionRepository _rolePermissionRepository;
        private readonly IUnitOfWork _unitOfWork;

        public RemovePermissionFromRoleHandler( IRolePermissionRepository rolePermissionRepository, IUnitOfWork unitOfWork)
        {
            _rolePermissionRepository = rolePermissionRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task Handle ( RemovePermissionFromRoleCommand request , CancellationToken cancellationToken)
        {
            var exists = await _rolePermissionRepository.ExistsAsync(request.RoleId , request.PermissionId , cancellationToken);

            if (!exists)
            {
                throw new KeyNotFoundException( "The specified role permission was not found.");
            }

            await _rolePermissionRepository.RemoveAsync( request.RoleId, request.PermissionId, cancellationToken);

            await _unitOfWork.SaveChangesAsync( cancellationToken);
        }
    }
}
