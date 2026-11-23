
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.Activate
{
    public sealed class ActiveUserHandler:IRequestHandler<ActivateUserCommand, UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActiveUserHandler (IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(ActivateUserCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new TeacherNotFoundException();
            }
            if (user.Status == UserStatus.Suspended || user.Status == UserStatus.Pending)
            {
                user.Activate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return user.ToDto();
        }
    }
}
