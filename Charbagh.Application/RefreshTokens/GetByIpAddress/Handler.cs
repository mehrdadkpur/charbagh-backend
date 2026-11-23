using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetByIpAddress
{
    public sealed class GetRefreshTokensByIpAddressHandler : IRequestHandler<GetRefreshTokensByIpAddressQuery ,IReadOnlyList< RefreshTokenDto>>
    {
        private readonly IRefreshTokenRepository _repository;

        public GetRefreshTokensByIpAddressHandler(IRefreshTokenRepository repository)
        {
            _repository = repository;
        }

        public async Task <IReadOnlyList< RefreshTokenDto>> Handle (GetRefreshTokensByIpAddressQuery request , CancellationToken cancellationToken)
        {
            var refreshTokens = await _repository.GetByIPAddressAsync(request.IpAddress , cancellationToken);

            return refreshTokens.Select(x => x.ToDto()).ToList();
        }
    }
}
