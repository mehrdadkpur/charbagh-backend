using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Block;
using Charbagh.Application.Users.Common;
using MediatR;


namespace Charbagh.Application.Users.Deactivate
{
    public sealed class BlockUserHandler:IRequestHandler<BlockUserCommand , UserDto>
    {
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public BlockUserHandler(IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle (BlockUserCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.id, cancellationToken);

            if(user == null)
            {
                throw new UserNotFoundException(request.id);
            }

            if(user.Status == UserStatus.Active)
            {
                user.Block();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return user.ToDto();
        }
    }
    
}
