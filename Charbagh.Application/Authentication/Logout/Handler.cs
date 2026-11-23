using Charbagh.Application.Abstractions.Persistence;
using MediatR;

namespace Charbagh.Application.Authentication.Logout;

public sealed class LogoutHandler
    : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(
        LogoutCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            throw new ArgumentException(
                "Refresh token is required.",
                nameof(request.RefreshToken));

        var refreshToken =
            await _refreshTokenRepository.GetForUpdateAsync(
                request.RefreshToken,
                cancellationToken);

        if (refreshToken is null)
            return;

        if (refreshToken.IsRevoked)
            return;

        refreshToken.Revoke();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);
    }
}