using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.SetRevoke
{
    public sealed class SetRevokeRefreshTokenHandler : IRequestHandler<SetRevokeRefreshTokenCommand , RefreshTokenDto >
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetRevokeRefreshTokenHandler(IRefreshTokenRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshTokenDto> Handle(SetRevokeRefreshTokenCommand request , CancellationToken cancellationToken)
        {
            var refreshToken = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (refreshToken == null)
            {
                throw new RefreshTokenNotfound(request.Id);
            }

            refreshToken.Revoke();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return refreshToken.ToDto();
        }
    }
}
