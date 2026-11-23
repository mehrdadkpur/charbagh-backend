using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.Update
{
    public sealed class UpdateRoleHandler : IRequestHandler<UpdateRoleCommand , RoleDto>
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateRoleHandler(IRoleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(UpdateRoleCommand request, CancellationToken cancellationToken)
        {
            var role = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if(role == null)
            {
                throw new RoleNotFoundException(request.Id);
            }

            var existsRole = await _repository.ExistsNameAsync(request.Name,request.Id, cancellationToken);

            if (existsRole)
            {
                throw new DuplicateRoleNameException(request.Name);
            }

            role.Update(request.Name, request.DisplayName, request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return role.ToDto();
        }
    }
}
