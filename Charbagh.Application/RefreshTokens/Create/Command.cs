
using Charbagh.Application.RefreshTokens.Common;
using MediatR;

namespace Charbagh.Application.RefreshTokens.Create
{
    public sealed record CreateRefreshTokenCommand(
        Guid UserId,
        string Token,
        DateTime ExpiresAt,
        string? IpAddress,
        string? UserAgent,
        string? DeviceName
        ) : IRequest<RefreshTokenDto>;
}
