using MediatR;

namespace Charbagh.Application.Authentication.RefreshToken;

public sealed record RefreshTokenCommand( string RefreshToken) : IRequest<RefreshTokenResponse>;