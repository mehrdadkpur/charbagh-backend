using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using MediatR;

namespace Charbagh.Application.Authentication.Logout;

public sealed class LogoutHandler : IRequestHandler<LogoutCommand>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ILoginHistoryRepository _loginHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;

    public LogoutHandler(
        IRefreshTokenRepository refreshTokenRepository,
        ILoginHistoryRepository loginHistoryRepository,
        IUnitOfWork unitOfWork)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _loginHistoryRepository = loginHistoryRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(LogoutCommand request, CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            throw new ArgumentException("Refresh token is required.", nameof(request.RefreshToken));

        var refreshToken = await _refreshTokenRepository.GetForUpdateAsync(
            request.RefreshToken, cancellationToken);

        if (refreshToken is null || refreshToken.IsRevoked)
            return;

        refreshToken.Revoke();

        if (refreshToken.LoginHistoryId.HasValue)
        {
            var history = await _loginHistoryRepository.GetForUpdateAsync(
                refreshToken.LoginHistoryId.Value, cancellationToken);
            history?.RecordLogout();
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
