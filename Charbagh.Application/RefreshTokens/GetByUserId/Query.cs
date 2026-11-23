using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetByUserId
{
    public sealed record GetRefreshTokensByUserIdQuery(Guid UserId) : IRequest<IReadOnlyList< RefreshTokenDto>>;
}
