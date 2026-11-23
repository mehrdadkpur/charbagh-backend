using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Roles.Common;
using MediatR;

namespace Charbagh.Application.Roles.GetByIsSystem
{
    public sealed class GetRolesByIsSystemHandler : IRequestHandler<GetRolesByIsSystemQuery, IReadOnlyList<RoleDto>>
    {
        private readonly IRoleRepository _roleRepository;


        public GetRolesByIsSystemHandler(IRoleRepository roleRepository)
        {
            _roleRepository = roleRepository;
        }

        public async Task<IReadOnlyList<RoleDto>> Handle(GetRolesByIsSystemQuery request, CancellationToken cancellationToken)
        {
            var roles = await _roleRepository.GetByIsSystemAsync(request.IsSystem,cancellationToken);

            return roles.Select(x => x.ToDto()).ToList();
        }
    }
}
