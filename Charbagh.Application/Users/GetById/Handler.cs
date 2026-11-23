using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.GetById
{
    public sealed class GetUserByIdHandler: IRequestHandler<GetUserByIdQuery , UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByIdHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle (GetUserByIdQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.Id , cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(request.Id);
            }
            return user.ToDto();
        }
    }
}
