using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public sealed class ServicePriceRepository : IServicePriceRepository
    {
        private readonly CharbaghDbContext _dbContext;

        public ServicePriceRepository(CharbaghDbContext charbaghDbContext)
        {
            _dbContext = charbaghDbContext;
        }

        public async Task AddAsync(
            ServicePrice servicePrice,
            CancellationToken cancellationToken)
        {
            await _dbContext.ServicePrices.AddAsync(
                servicePrice,
                cancellationToken);
        }

        public async Task<ServicePrice?> GetByIdAsync(
            Guid id,
            CancellationToken cancellationToken)
        {
            return await _dbContext.ServicePrices
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

        public async Task<IReadOnlyList<ServicePrice>> GetAllAsync(
            CancellationToken cancellationToken)
        {
            return await _dbContext.ServicePrices
                .AsNoTracking()
                .OrderBy(x => x.EducationServiceId)
                .ThenByDescending(x => x.EffectiveFrom)
                .ToListAsync(cancellationToken);
        }

        public async Task<bool> ExistsActiveByEducationServiceIdAsync(
            Guid educationServiceId,
            Guid? excludeId,
            CancellationToken cancellationToken)
        {
            return await _dbContext.ServicePrices
                .AnyAsync(
                    x => x.EducationServiceId == educationServiceId
                         && x.IsActive
                         && (!excludeId.HasValue
                             || x.Id != excludeId.Value),
                    cancellationToken);
        }

        public async Task<bool> HasDateOverlapAsync(Guid educationServiceId, DateOnly effectiveFrom, DateOnly? effectiveTo, Guid? excludeId, CancellationToken cancellationToken)
        {
            return await _dbContext.ServicePrices
                 .AnyAsync(
                     x => x.EducationServiceId == educationServiceId
                     && x.EffectiveFrom == effectiveFrom
                     && x.EffectiveTo == effectiveTo
                     && (!excludeId.HasValue || x.Id != excludeId.Value) , cancellationToken);
        }
    }
}