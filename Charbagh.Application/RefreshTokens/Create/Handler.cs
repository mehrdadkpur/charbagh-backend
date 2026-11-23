using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.RefreshTokens.Common;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.RefreshTokens.Create
{
    public sealed class CreateRefreshTokenHandler : IRequestHandler<CreateRefreshTokenCommand , RefreshTokenDto>
    {
        private readonly IRefreshTokenRepository _refreshTokenRepository;
        private readonly IUserRepository _userRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateRefreshTokenHandler (IRefreshTokenRepository refreshTokenRepository , IUserRepository userRepository , IUnitOfWork unitOfWork)
        {
            _refreshTokenRepository = refreshTokenRepository;
            _userRepository = userRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshTokenDto> Handle (CreateRefreshTokenCommand request , CancellationToken cancellationToken)
        {
            var user = await _userRepository.GetByIdAsync(request.UserId, cancellationToken);

            if (user == null)
            {
                throw new UserNotFoundException(request.UserId);
            }

            var refreshToken = RefreshToken.Create(request.UserId, request.Token, request.ExpiresAt, request.IpAddress, request.UserAgent, request.DeviceName);

            await _refreshTokenRepository.AddAsync(refreshToken , cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return refreshToken.ToDto();
        }
    }
}
