
using Charbagh.Domain.Entities.Settings;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IEducationServiceRepository
    {
        Task AddAsync(EducationService educationService, CancellationToken cancellationToken);

        Task<EducationService?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<EducationService>> GetAllAsync(CancellationToken cancellationToken);

        Task<EducationService?> GetByServiceNameAsync(string ServiceName, CancellationToken cancellationToken);

        Task<bool> ExistsByNameAsync(string serviceName, Guid? excludeId,CancellationToken cancellationToken);
    }

}
