using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Authentication.RefreshToken;

public sealed class RefreshTokenHandler
    : IRequestHandler<RefreshTokenCommand, RefreshTokenResponse>
{
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUserRepository _userRepository;
    private readonly IUserBranchRepository _userBranchRepository;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public RefreshTokenHandler(
        IRefreshTokenRepository refreshTokenRepository,
        IUserRepository userRepository,
        IUserBranchRepository userBranchRepository,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IUnitOfWork unitOfWork,
        JwtSettings jwtSettings)
    {
        _refreshTokenRepository = refreshTokenRepository;
        _userRepository = userRepository;
        _userBranchRepository = userBranchRepository;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _unitOfWork = unitOfWork;
        _jwtSettings = jwtSettings;
    }

    public async Task<RefreshTokenResponse> Handle(
        RefreshTokenCommand request,
        CancellationToken cancellationToken)
    {
        if (string.IsNullOrWhiteSpace(request.RefreshToken))
            throw new ArgumentException(
                "Refresh token is required.",
                nameof(request.RefreshToken));

        var currentRefreshToken =
            await _refreshTokenRepository.GetForUpdateAsync(
                request.RefreshToken,
                cancellationToken);

        if (currentRefreshToken is null)
            throw new UnauthorizedAccessException(
                "Invalid refresh token.");

        if (!currentRefreshToken.IsActive)
            throw new UnauthorizedAccessException(
                "Refresh token is expired or revoked.");

        var user =
            await _userRepository.GetByIdAsync(
                currentRefreshToken.UserId,
                cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException(
                "User associated with refresh token was not found.");

        var userBranch = await _userBranchRepository.GetDefaultByUserIdAsync(user.Id, cancellationToken);

        if(userBranch is null)
        {
            throw new UserBranchNotFoundException(user.Id);
        }

        var branchId = userBranch.BranchId;

        var newAccessToken =
            _jwtTokenGenerator.GenerateToken(
                user.Id,
                branchId,
                user.Mobile,
                user.FullName,
                user.SecurityStamp);

        var newRefreshTokenValue =
            _refreshTokenGenerator.Generate();

        var newRefreshTokenExpiresAt =
            DateTime.UtcNow.AddDays(
                _jwtSettings.RefreshTokenDays);

        var newRefreshToken =
            Charbagh.Domain.Entities.Identity.RefreshToken.Create(
                user.Id,
                newRefreshTokenValue,
                newRefreshTokenExpiresAt,
                currentRefreshToken.IpAddress,
                currentRefreshToken.UserAgent,
                currentRefreshToken.DeviceName,
                currentRefreshToken.LoginHistoryId);

        currentRefreshToken.Replace(
            newRefreshTokenValue);

        await _refreshTokenRepository.AddAsync(
            newRefreshToken,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new RefreshTokenResponse(
            newAccessToken,
            newRefreshTokenValue);
    }
}