using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetByIpAddress
{
    public sealed record GetRefreshTokensByIpAddressQuery(string IpAddress) : IRequest<IReadOnlyList< RefreshTokenDto>>;
}
