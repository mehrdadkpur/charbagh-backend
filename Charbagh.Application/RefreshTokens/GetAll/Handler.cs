using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetAll
{
    public sealed class GetAllRefreshTokensHandler : IRequestHandler<GetAllRefreshTokensQuery ,IReadOnlyList< RefreshTokenDto>>
    {
        private readonly IRefreshTokenRepository _repository;

        public GetAllRefreshTokensHandler(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList< RefreshTokenDto>> Handle(GetAllRefreshTokensQuery request , CancellationToken cancellationToken)
        {
            var refreshTokens = await _repository.GetAllAsync(cancellationToken);

            return refreshTokens.Select(x => x.ToDto()).ToList();
        }
    }
}
