namespace Charbagh.Application.Abstractions.Security;

public interface IJwtTokenGenerator
{
    string GenerateToken(
        Guid userId,
        Guid branchId,
        string mobile,
        string fullName,
        string securityStamp);
}