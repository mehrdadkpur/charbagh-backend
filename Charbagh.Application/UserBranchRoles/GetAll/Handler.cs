using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetAll
{
    public sealed class GetAllUserBranchRolesHandler : IRequestHandler<GetAllUserBranchRolesQuery ,IReadOnlyList< UserBranchRoleDto>>
    {
        private readonly IUserBranchRoleRepository _repository;

        public GetAllUserBranchRolesHandler(IUserBranchRoleRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchRoleDto>> Handle (GetAllUserBranchRolesQuery request , CancellationToken cancellationToken)
        {
            var userBranchRoles = await _repository.GetAllAsync (cancellationToken);

            return userBranchRoles.Select(x => x.ToDto()).ToList();

        }
    }
}
