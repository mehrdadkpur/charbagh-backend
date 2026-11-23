using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranches.Common;
using MediatR;

namespace Charbagh.Application.UserBranches.RemoveUserFromBranch
{
    public sealed class RemoveUserFromBranchHandler : IRequestHandler<RemoveUserFromBranchCommand, UserBranchDto>
    {
        private readonly IUserBranchRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public RemoveUserFromBranchHandler(IUnitOfWork unitOfWork, IUserBranchRepository repository)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task <UserBranchDto> Handle(RemoveUserFromBranchCommand request , CancellationToken cancellationToken)
        {
            var userBranch = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (userBranch == null)
            {
                throw new UserBranchNotFoundException(request.Id);
            }

            userBranch.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userBranch.ToDto();
        }
    }
}
