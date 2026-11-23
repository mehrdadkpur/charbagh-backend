using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.SetReplace
{
    public sealed record SetReplaceRefreshTokenCommand(Guid Id , string ReplaceByToken): IRequest<RefreshTokenDto>;
}
