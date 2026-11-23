using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranches.Common;
using Charbagh.Application.UserBranchRoles.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.UserBranches.Assign
{
    public sealed class AssignUserToBranchHandler : IRequestHandler<AssignUserToBranchCommand, UserBranchDto>
    {
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IUserRepository _userRepository;
        private readonly IBranchRepository _branchRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignUserToBranchHandler(
            IUserBranchRepository userBranchRepository,
            IUserRepository userRepository,
            IBranchRepository branchRepository,
            IUnitOfWork unitOfWork
            )
        {
            _userBranchRepository = userBranchRepository;
            _userRepository = userRepository;
            _branchRepository = branchRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <UserBranchDto> Handle (AssignUserToBranchCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);
            var branch = await _branchRepository.GetByIdAsync (request.BranchId, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            if(branch == null)
            {
                throw new BranchNotFoundException(request.BranchId);
            }

            var exists = await _userBranchRepository.ExistsAsync(request.UserId, request.BranchId, cancellationToken); 
            
            if (exists)
            {
                throw new DuplicateUserBranchException(request.UserId, request.BranchId);
            }

            if (request.IsDefault)
            {
                var currentDefault =
                    await _userBranchRepository.GetDefaultAsync(
                        request.UserId,
                        request.BranchId,
                        cancellationToken);

                currentDefault?.RemoveFromDefault();
            }

            var userBranch = UserBranch.Create(request.UserId, request.BranchId , request.AssignedBy);

            await _userBranchRepository.AddAsync(userBranch , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userBranch.ToDto();
        }
    }
}
