namespace Charbagh.Application.Users.ResetPassword;

public sealed record ResetUserPasswordResponse(
    Guid UserId,
    string TemporaryPassword);
