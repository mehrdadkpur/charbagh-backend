using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetAll
{
    public sealed record GetAllRefreshTokensQuery() : IRequest<IReadOnlyList< RefreshTokenDto>>;
}
