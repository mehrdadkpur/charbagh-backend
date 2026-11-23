using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.SetReplace
{
    public sealed class SetReplaceRefreshTokenHandler : IRequestHandler<SetReplaceRefreshTokenCommand , RefreshTokenDto >
    {
        private readonly IRefreshTokenRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetReplaceRefreshTokenHandler(IRefreshTokenRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<RefreshTokenDto> Handle(SetReplaceRefreshTokenCommand request , CancellationToken cancellationToken)
        {
            var refreshToken = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (refreshToken == null)
            {
                throw new RefreshTokenNotfound(request.Id);
            }

            refreshToken.Replace(request.ReplaceByToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return refreshToken.ToDto();
        }
    }
}
