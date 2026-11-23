
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Roles.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Roles.Create
{
    public sealed class CreateRoleHandler : IRequestHandler<CreateRoleCommand , RoleDto>
    {
        private readonly IRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRoleHandler(IRoleRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RoleDto> Handle(CreateRoleCommand request , CancellationToken cancellationToken)
        {
            var existsRole = await _repository.ExistsNameAsync(request.Name,null, cancellationToken);

            if (existsRole)
            {
                throw new DuplicateRoleNameException(request.Name);
            }

            var role = Role.Create(request.Name, request.DisplayName, request.Description, request.IsSystem);

            await _repository.AddAsync(role , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return role.ToDto();
        }
    }
}
