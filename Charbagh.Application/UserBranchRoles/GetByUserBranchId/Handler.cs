using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetByUserId
{
    public sealed class GetUserBranchRolesByUserIdHandler : IRequestHandler<GetUserBranchRolesByUserIdQuery ,IReadOnlyList< UserBranchRoleDto>>
    {
        private readonly IUserBranchRoleRepository _repository;

        public GetUserBranchRolesByUserIdHandler(IUserBranchRoleRepository repository)
        {
            _repository = repository; 
        }

        public async Task <IReadOnlyList< UserBranchRoleDto>> Handle (GetUserBranchRolesByUserIdQuery request , CancellationToken cancellationToken)
        {
            var userBranchRoles = await _repository.GetByUserBranchIdAsync (request.UserId,cancellationToken);

            return userBranchRoles.Select(x => x.ToDto()).ToList();

        }
    }
}
