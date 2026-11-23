using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using Charbagh.Application.Users.Suspend;
using MediatR;


namespace Charbagh.Application.Users.Deactivate
{
    public sealed class DeactivateUserHandler:IRequestHandler<SuspendUserCommand , UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateUserHandler (IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle (SuspendUserCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.id, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException(request.id);
            }

            if(user.Status == UserStatus.Active)
            {
                user.Suspend();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return user.ToDto();
        }
    }
    
}
