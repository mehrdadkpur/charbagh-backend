using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.SetRevoke
{
    public sealed record SetRevokeRefreshTokenCommand(Guid Id): IRequest<RefreshTokenDto>;
}
