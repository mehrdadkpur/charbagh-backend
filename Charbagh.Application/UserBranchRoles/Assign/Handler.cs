using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.UserBranchRoles.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.UserBranchRoles.Assign
{
    public sealed class AssignHandler : IRequestHandler<AssignCommand , UserBranchRoleDto>
    {
        private readonly IUserBranchRoleRepository _userBranchRoleRepository;
        private readonly IUserBranchRepository _userBranchRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly IUnitOfWork _unitOfWork;

        public AssignHandler(
            IUserBranchRoleRepository userBranchRoleRepository,
            IUserBranchRepository userBranchRepository,
            IRoleRepository roleRepository,
            IUnitOfWork unitOfWork
            )
        {
            _userBranchRoleRepository = userBranchRoleRepository;
            _userBranchRepository = userBranchRepository;
            _roleRepository = roleRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <UserBranchRoleDto> Handle (AssignCommand request , CancellationToken cancellationToken)
        {
            var userBranch = await _userBranchRepository.GetByIdAsync(request.UserBranchId, cancellationToken);
            var role = await _roleRepository.GetByIdAsync(request.RoleId, cancellationToken);

            if(userBranch == null)
            {
                throw new UserBranchNotFoundException(request.UserBranchId);
            }

            if(role == null)
            {
                throw new RoleNotFoundException(request.RoleId);
            }

            var exists = await _userBranchRoleRepository.ExistsAsync(request.UserBranchId, request.RoleId, cancellationToken); 
            
            if (exists)
            {
                throw new DuplicateUserBranchRoleException(request.UserBranchId, request.RoleId);
            }

            var userBranchRole = UserBranchRole.Create(request.UserBranchId, request.RoleId , request.AssignedBy );

            await _userBranchRoleRepository.AddAsync(userBranchRole , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return userBranchRole.ToDto();
        }
    }
}
