
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Deactivate
{
    public sealed class DeactiveRoleHandler:IRequestHandler<DeactivateRoleCommand, RoleDto>
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactiveRoleHandler (IRoleRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(DeactivateRoleCommand request , CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
            {
                throw new RoleNotFoundException(request.Id);
            }
            if (role.IsActive)
            {
                role.Deactivate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return role.ToDto();
        }
    }
}
