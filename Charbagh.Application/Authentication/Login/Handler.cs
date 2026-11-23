using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Persistence.Repositories.Identity;
using Charbagh.Application.Abstractions.Security;
using Charbagh.Application.Authentication.Login;
using Charbagh.Domain.Entities.Identity;
using MediatR;

public sealed class LoginHandler: IRequestHandler<LoginCommand, LoginResponse>
{
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

        var user = await _userRepository.GetByMobileAsync( request.Mobile, cancellationToken);

        if (user is null)
            throw new UnauthorizedAccessException("Invalid mobile number or password.");

        var isPasswordValid = _passwordHasher.VerifyPassword( request.Password, user.PasswordHash, user.PasswordSalt);

        if (!isPasswordValid)
            throw new UnauthorizedAccessException( "Invalid mobile number or password.");

        var userBranch = await _userBranchRepository.GetDefaultByUserIdAsync( user.Id, cancellationToken);

        if (userBranch is null)
        {
            throw new InvalidOperationException( "No default branch is assigned to this user.");
        }

        var branchId = userBranch.BranchId;

        var accessToken = _jwtTokenGenerator.GenerateToken(
            user.Id,
            branchId,
            user.Mobile,
            user.FullName,
            user.SecurityStamp);


        var refreshTokenValue =
            _refreshTokenGenerator.Generate();

        var refreshTokenExpiresAt = DateTime.UtcNow.AddDays(_jwtSettings.RefreshTokenDays);

        var refreshToken = RefreshToken.Create(
            user.Id,
            refreshTokenValue,
            refreshTokenExpiresAt);

        await _refreshTokenRepository.AddAsync(
            refreshToken,
            cancellationToken);

        var loginHistory = LoginHistory.Create( user.Id, isSuccessful: true);

        await _loginHistoryRepository.AddAsync(
            loginHistory,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return new LoginResponse(
            user.Id,
            branchId,
            user.FirstName,
            user.LastName,
            user.Mobile,
            user.FullName,
            accessToken,
            refreshTokenValue
            );
    }
}