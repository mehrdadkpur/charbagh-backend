using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranchRoles.Common;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.RemoveRoleFromUser
{
    public sealed class RemoveRoleFromUserHandler : IRequestHandler<RemoveRoleFromUserCommand, UserBranchRoleDto>
    {
        private readonly IUserBranchRoleRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveRoleFromUserHandler(IUnitOfWork unitOfWork, IUserBranchRoleRepository repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task <UserBranchRoleDto> Handle(RemoveRoleFromUserCommand request , CancellationToken cancellationToken)
        {
            var userBranchRole = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (userBranchRole == null)
            {
                throw new UserBranchRoleNotFoundException(request.Id);
            }

            userBranchRole.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userBranchRole.ToDto();
        }
    }
}
