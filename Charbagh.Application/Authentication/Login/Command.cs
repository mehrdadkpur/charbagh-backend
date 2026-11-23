using MediatR;

namespace Charbagh.Application.Authentication.Login;

public sealed record LoginCommand( string Mobile, string Password) : IRequest<LoginResponse>;