using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IUserRepository
{
    Task AddAsync(User user, CancellationToken cancellationToken);
    Task<User?> GetByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<User?> GetDetailsByIdAsync(Guid id, CancellationToken cancellationToken);
    Task<List<User>> GetAllAsync(CancellationToken cancellationToken);
    Task<User?> GetByNationalIdentificationNumberAsync(string nationalIdentificationNumber, CancellationToken cancellationToken);
    Task<User?> GetByMobileAsync(string mobile, CancellationToken cancellationToken);
    Task<bool> ExistsNationalIdentificationNumberAsync(string nationalIdentificationNumber, Guid? excludeUserId, CancellationToken cancellationToken);
    Task<bool> ExistsNationalityIdAsync(Guid nationalityId, CancellationToken cancellationToken);
    Task<bool> ExistsByUserIdAsync(Guid id, CancellationToken cancellationToken);
    Task<bool> ExistsByMobileAsync(string mobile, CancellationToken cancellationToken);
    Task<int> CountAsync(CancellationToken cancellationToken);
}
