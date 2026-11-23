using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IServicePriceRepository
    {
        Task AddAsync(ServicePrice servicePrice, CancellationToken cancellationToken);

        Task<ServicePrice?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<ServicePrice>> GetAllAsync(CancellationToken cancellationToken);

        Task<bool> ExistsActiveByEducationServiceIdAsync(Guid educationServiceId, Guid? excludeId, CancellationToken cancellationToken);

        Task<bool> HasDateOverlapAsync( Guid educationServiceId, DateOnly effectiveFrom, DateOnly? effectiveTo, Guid? excludeId, CancellationToken cancellationToken);
    }

}
