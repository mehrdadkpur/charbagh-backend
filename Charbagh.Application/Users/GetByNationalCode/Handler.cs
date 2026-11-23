using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Users.Common;
using Charbagh.Application.Users.GetByNationalCode;
using MediatR;

namespace Charbagh.Application.Users.GetById
{
    public sealed class GetUserByNationalCodeHandler : IRequestHandler<GetUserByNationalCodeQuery, UserDto>
    {
        private readonly IUserRepository _userRepository;

        public GetUserByNationalCodeHandler(IUserRepository userRepository)
        {
            _userRepository = userRepository;
        }

        public async Task<UserDto> Handle (GetUserByNationalCodeQuery request, CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByNationalIdentificationNumberAsync(request.NationalCode , cancellationToken);

            if (user == null)
            {
                throw new Exception("National Code Not Found!");
            }
            return user.ToDto();
        }
    }
}
