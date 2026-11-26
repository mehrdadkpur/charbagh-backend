using System.Security.Cryptography;
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Abstractions.Security;
using MediatR;

namespace Charbagh.Application.Users.ResetPassword;

public sealed class ResetUserPasswordHandler
    : IRequestHandler<ResetUserPasswordCommand, ResetUserPasswordResponse>
{
    private readonly IUserRepository _userRepository;
    private readonly IPasswordHasher _passwordHasher;
    private readonly IRefreshTokenRepository _refreshTokenRepository;
    private readonly IUnitOfWork _unitOfWork;

    public ResetUserPasswordHandler(
        IUserRepository userRepository,
        IPasswordHasher passwordHasher,
        IRefreshTokenRepository refreshTokenRepository,
        IUnitOfWork unitOfWork)
    {
        _userRepository = userRepository;
        _passwordHasher = passwordHasher;
        _refreshTokenRepository = refreshTokenRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<ResetUserPasswordResponse> Handle(
        ResetUserPasswordCommand request,
        CancellationToken cancellationToken)
    {
        var user = await _userRepository.GetByIdAsync(
            request.UserId,
            cancellationToken);

        if (user is null)
            throw new InvalidOperationException("User was not found.");

        // A cryptographically random temporary password is generated and shown only once.
        var temporaryPassword = Convert.ToBase64String(
                RandomNumberGenerator.GetBytes(24))
            .TrimEnd('=')
            .Replace('+', '-')
            .Replace('/', '_');

        var (hash, salt) = _passwordHasher.HashPassword(temporaryPassword);
        user.SetPassword(hash, salt);

        // Revoke all active refresh sessions after resetting the password.
        var refreshTokens = await _refreshTokenRepository.GetByUserIdForUpdateAsync(
            user.Id,
            cancellationToken);

        foreach (var refreshToken in refreshTokens)
        {
            if (refreshToken.IsActive)
                refreshToken.Revoke();
        }

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return new ResetUserPasswordResponse(
            user.Id,
            temporaryPassword);
    }
}
