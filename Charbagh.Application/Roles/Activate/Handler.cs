
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Activate
{
    public sealed class ActiveRoleHandler:IRequestHandler<ActivateRoleCommand, RoleDto>
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActiveRoleHandler (IRoleRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(ActivateRoleCommand request , CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (role == null)
            {
                throw new RoleNotFoundException(request.Id);
            }
            if (!role.IsActive)
            {
                role.Activate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return role.ToDto();
        }
    }
}
