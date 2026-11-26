using MediatR;

namespace Charbagh.Application.Users.ResetPassword;

public sealed record ResetUserPasswordCommand(Guid UserId)
    : IRequest<ResetUserPasswordResponse>;
