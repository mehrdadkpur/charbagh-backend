using MediatR;

namespace Charbagh.Application.Authentication.Login;

public sealed record LoginCommand(
    string Mobile,
    string Password,
    string? IpAddress = null,
    string? UserAgent = null) : IRequest<LoginResponse>;