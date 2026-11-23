using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.GetById
{
    public sealed class GetUserBranchRoleByIdHandler : IRequestHandler<GetUserBranchRoleByIdQuery , UserBranchRoleDto>
    {
        private readonly IUserBranchRoleRepository _repository;

        public GetUserBranchRoleByIdHandler(IUserBranchRoleRepository repository)
        {
            _repository = repository;
        }

        public async Task <UserBranchRoleDto> Handle(GetUserBranchRoleByIdQuery request , CancellationToken cancellationToken)
        {
            var userBranchRole = await _repository.GetByIdAsync(request.Id , cancellationToken);

            if (userBranchRole == null)
            {
                throw new UserBranchRoleNotFoundException(request.Id);
            }

            return userBranchRole.ToDto();
        }
    }
}
