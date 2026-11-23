using MediatR;

namespace Charbagh.Application.Authentication.Logout;

public sealed record LogoutCommand( string RefreshToken) : IRequest;