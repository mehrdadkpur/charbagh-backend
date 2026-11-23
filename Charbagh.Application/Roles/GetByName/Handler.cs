using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Roles.Common;
using Charbagh.Application.Roles.GetByName;
using MediatR;

namespace Charbagh.Application.Roles.GetByIsName
{
    public sealed class GetRolesByNameHandler : IRequestHandler<GetRolesByNameQuery, IReadOnlyList<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;


        public GetRolesByNameHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IReadOnlyList<RoleDto>> Handle(GetRolesByNameQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetByNameAsync(request.Name,cancellationToken);

            return roles.Select(x => x.ToDto()).ToList();
        }
    }
}
