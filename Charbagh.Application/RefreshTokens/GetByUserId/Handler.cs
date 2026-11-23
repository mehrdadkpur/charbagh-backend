using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetByUserId
{
    public sealed class GetRefreshTokensByUserIdHandler : IRequestHandler<GetRefreshTokensByUserIdQuery ,IReadOnlyList< RefreshTokenDto>>
    {
        private readonly IRefreshTokenRepository _repository;

        public GetRefreshTokensByUserIdHandler(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task <IReadOnlyList< RefreshTokenDto>> Handle (GetRefreshTokensByUserIdQuery request , CancellationToken cancellationToken)
        {
            var refreshTokens = await _repository.GetByUserIdAsync(request.UserId , cancellationToken);

            return refreshTokens.Select(x => x.ToDto()).ToList();
        }
    }
}
