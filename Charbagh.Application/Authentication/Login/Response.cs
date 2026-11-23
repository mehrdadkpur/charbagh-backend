namespace Charbagh.Application.Authentication.Login;

public sealed record LoginResponse(
    Guid UserId,
    Guid BranchId,
    string FirstName,
    string LastName,
    string Mobile,
    string FullName,
    string AccessToken,
    string RefreshToken);