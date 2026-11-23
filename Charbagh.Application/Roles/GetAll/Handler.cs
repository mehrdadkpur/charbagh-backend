using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetAll
{
    public sealed class GetAllRolesHandler : IRequestHandler<GetAllRolesQuery ,IReadOnlyList< RoleDto>>
    {
        private readonly IRoleRepository _repository;

        public GetAllRolesHandler(IRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< RoleDto>> Handle(GetAllRolesQuery request , CancellationToken cancellationToken)
        {
            var roles = await _repository.GetAllAsync(cancellationToken);

            return roles.Select(x => x.ToDto()).ToList();
        }
    }
}
