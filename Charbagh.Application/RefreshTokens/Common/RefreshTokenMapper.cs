using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.RefreshTokens.Common
{
    public static class RefreshTokenMapper
    {
        public static RefreshTokenDto ToDto(this RefreshToken refreshToken)
        {
            return new RefreshTokenDto(
                refreshToken.Id, 
                refreshToken.UserId, 
                refreshToken.Token, 
                refreshToken.ExpiresAt, 
                refreshToken.RevokedAt, 
                refreshToken.ReplacedByToken, 
                refreshToken.IpAddress, 
                refreshToken.UserAgent, 
                refreshToken.DeviceName
                );
        }
    }
}
