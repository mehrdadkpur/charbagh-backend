using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence.Repositories.Identity;

public interface ILoginHistoryRepository
{
    Task AddAsync(
        LoginHistory loginHistory,
        CancellationToken cancellationToken);

    Task<LoginHistory?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<LoginHistory>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<LoginHistory>> GetByUserIdAsync(
        Guid userId,
        CancellationToken cancellationToken);

    Task<LoginHistory?> GetForUpdateAsync(
    Guid id,
    CancellationToken cancellationToken);
}