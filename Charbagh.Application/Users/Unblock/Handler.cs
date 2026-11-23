
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using Charbagh.Application.Users.Unblock;
using MediatR;

namespace Charbagh.Application.Users.Activate
{
    public sealed class UnblockUserHandler:IRequestHandler<UnblockUserCommand , UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UnblockUserHandler(IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<UserDto> Handle(UnblockUserCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id, cancellationToken);
            if (user == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            if (user.Status == UserStatus.Blocked)
            {
                user.Unblock();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return user.ToDto();
        }
    }
}
