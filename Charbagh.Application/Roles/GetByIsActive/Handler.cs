using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetByIsActive
{
    public sealed class GetRolesByIsActiveHandler : IRequestHandler<GetRolesByIsActiveQuery, IReadOnlyList<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;


        public GetRolesByIsActiveHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IReadOnlyList<RoleDto>> Handle(GetRolesByIsActiveQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetByIsSystemAsync(request.IsActive,cancellationToken);

            return roles.Select(x => x.ToDto()).ToList();
        }
    }
}
