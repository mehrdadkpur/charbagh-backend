using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.GetById
{
    public sealed record GetRefreshTokenByIdQuery(Guid Id) : IRequest<RefreshTokenDto>;
}
