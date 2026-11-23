using System.Security.Cryptography;
using Charbagh.Application.Abstractions.Security;

namespace Charbagh.Infrastructure.Security;

public sealed class PasswordHasher : IPasswordHasher
{
    private const int SaltSize = 16;
    private const int HashSize = 32;
    private const int Iterations = 100_000;

    public (byte[] Hash, byte[] Salt) HashPassword(string password)
    {
        if (string.IsNullOrWhiteSpace(password))
            throw new ArgumentException(
                "Password is required.",
                nameof(password));

        var salt = RandomNumberGenerator.GetBytes(SaltSize);

        var hash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            salt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return (hash, salt);
    }

    public bool VerifyPassword(
        string password,
        byte[] storedHash,
        byte[] storedSalt)
    {
        if (string.IsNullOrWhiteSpace(password))
            return false;

        if (storedHash is null || storedHash.Length == 0)
            return false;

        if (storedSalt is null || storedSalt.Length == 0)
            return false;

        var computedHash = Rfc2898DeriveBytes.Pbkdf2(
            password,
            storedSalt,
            Iterations,
            HashAlgorithmName.SHA256,
            HashSize);

        return CryptographicOperations.FixedTimeEquals(
            computedHash,
            storedHash);
    }
}