using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetByRoleId
{
    public sealed class GetUserBranchRolesByRoleIdHandler : IRequestHandler<GetUserBranchRolesByRoleIdQuery ,IReadOnlyList< UserBranchRoleDto>>
    {
        private readonly IUserBranchRoleRepository _repository;

        public GetUserBranchRolesByRoleIdHandler(IUserBranchRoleRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchRoleDto>> Handle (GetUserBranchRolesByRoleIdQuery request , CancellationToken cancellationToken)
        {
            var userBranchRoles = await _repository.GetByRoleIdAsync (request.RoleId,cancellationToken);

            return userBranchRoles.Select(x => x.ToDto()).ToList();

        }
    }
}
