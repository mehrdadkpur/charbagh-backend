using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IRefreshTokenRepository
    {
        Task AddAsync(RefreshToken refreshToken, CancellationToken cancellationToken);

        Task<RefreshToken?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<RefreshToken?> GetForUpdateAsync(string token, CancellationToken cancellationToken);

        Task<IReadOnlyList<RefreshToken>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<RefreshToken>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<IReadOnlyList<RefreshToken>> GetByUserIdForUpdateAsync(Guid userId, CancellationToken cancellationToken);

        Task<IReadOnlyList<RefreshToken>> GetByIPAddressAsync(string ipAddress, CancellationToken cancellationToken);
    }
}
