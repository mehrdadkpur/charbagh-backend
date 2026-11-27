using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Authentication.Login;
using Charbagh.Domain.Entities.Identity;
using MediatR;

namespace Charbagh.Application.Authentication.Login;

public sealed class LoginHandler : IRequestHandler<LoginCommand, LoginResponse>
{
    private const string InvalidCredentials = "Invalid mobile number or password.";
    private readonly IUserRepository _userRepository;
    private readonly IUserBranchRepository _userBranchRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IJwtTokenGenerator _jwtTokenGenerator;
    private readonly IRefreshTokenGenerator _refreshTokenGenerator;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly ILoginHistoryRepository _loginHistoryRepository;
    private readonly IUnitOfWork _unitOfWork;
    private readonly JwtSettings _jwtSettings;

    public LoginHandler(
        IUserRepository userRepository,
        IUserBranchRepository userBranchRepository,
        IPasswordHasher passwordHasher,
        IJwtTokenGenerator jwtTokenGenerator,
        IRefreshTokenGenerator refreshTokenGenerator,
        IRefreshTokenRepository refreshTokenRepository,
        ILoginHistoryRepository loginHistoryRepository,
        JwtSettings jwtSettings,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _userBranchRepository = userBranchRepository;
        _passwordHasher = passwordHasher;
        _jwtTokenGenerator = jwtTokenGenerator;
        _refreshTokenGenerator = refreshTokenGenerator;
        _refreshTokenRepository = refreshTokenRepository;
        _loginHistoryRepository = loginHistoryRepository;
        _jwtSettings = jwtSettings;
        _unitOfWork = unitOfWork;
    }

    public async Task<LoginResponse> Handle(LoginCommand request, CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByMobileAsync(request.Mobile, cancellationToken);

        if (user is null)
        {
            await RecordFailedAttemptAsync(null, request, InvalidCredentials, cancellationToken);
            throw new UnauthorizedAccessException(InvalidCredentials);
        }

        var isPasswordValid = _passwordHasher.VerifyPassword(
            request.Password, user.PasswordHash, user.PasswordSalt);

        if (!isPasswordValid)
        {
            await RecordFailedAttemptAsync(user.Id, request, InvalidCredentials, cancellationToken);
            throw new UnauthorizedAccessException(InvalidCredentials);
        }

        var userBranch = await _userBranchRepository.GetDefaultByUserIdAsync(user.Id, cancellationToken);
        if (userBranch is null)
        {
            const string reason = "No default branch is assigned to this user.";
            await RecordFailedAttemptAsync(user.Id, request, reason, cancellationToken);
            throw new InvalidOperationException(reason);
        }

        var branchId = userBranch.BranchId;
        var accessToken = _jwtTokenGenerator.GenerateToken(
            user.Id, branchId, user.Mobile, user.FullName, user.SecurityStamp);

        var loginHistory = LoginHistory.Create(
            user.Id, request.Mobile, isSuccessful: true,
            ipAddress: request.IpAddress, userAgent: request.UserAgent);

        var refreshTokenValue = _refreshTokenGenerator.Generate();
        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);
        var refreshToken = RefreshToken.Create(
            user.Id, refreshTokenValue, refreshTokenExpiresAt,
            request.IpAddress, request.UserAgent, loginHistoryId: loginHistory.Id);

        await _loginHistoryRepository.AddAsync(loginHistory, cancellationToken);
        await _refreshTokenRepository.AddAsync(refreshToken, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new LoginResponse(
            user.Id, branchId, user.FirstName, user.LastName, user.Mobile,
            user.FullName, accessToken, refreshTokenValue);
    }

    private async Task RecordFailedAttemptAsync(
        Guid? userId, LoginCommand request, string reason, CancellationToken cancellationToken)
    {
        var history = LoginHistory.Create(
            userId,
            request.Mobile,
            isSuccessful: false,
            failureReason: reason,
            ipAddress: request.IpAddress,
            userAgent: request.UserAgent);

        await _loginHistoryRepository.AddAsync(history, cancellationToken);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
