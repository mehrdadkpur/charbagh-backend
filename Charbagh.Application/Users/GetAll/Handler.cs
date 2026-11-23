
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Users.Common;
using MediatR;

namespace Charbagh.Application.Users.GetAll
{
    public sealed class GetAllUsersHandler:IRequestHandler<GetAllUsersQuery , IReadOnlyList<UserDto>>
    {
        private readonly IUserRepository _userRepository;

        public GetAllUsersHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<IReadOnlyList<UserDto>> Handle (GetAllUsersQuery request , CancellationToken cancellationToken)
        {
            var users = await _userRepository.GetAllAsync (cancellationToken);

            return users
                .Select(x => x.ToDto())
                .ToList();
        }
    }
}
