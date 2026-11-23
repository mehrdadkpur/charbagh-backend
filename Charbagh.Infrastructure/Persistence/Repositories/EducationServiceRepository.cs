
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Settings;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class EducationServiceRepository : IEducationServiceRepository
    {

        private readonly CharbaghDbContext _dbContext;

        public EducationServiceRepository(CharbaghDbContext charbaghDbContext)
        {
            _dbContext = charbaghDbContext;
        }
        public async Task AddAsync(EducationService educationService, CancellationToken cancellationToken)
        {
            await _dbContext.EducationServices.AddAsync(educationService, cancellationToken);
        }


        public async Task<bool> ExistsByNameAsync(string serviceName,Guid? excludeId,CancellationToken cancellationToken)
        {
            var query = _dbContext.EducationServices.AsQueryable();

            if (excludeId.HasValue)
            {
                query = query.Where(x => x.Id != excludeId.Value);
            }

            return await query.AnyAsync(
                x => x.ServiceName == serviceName,
                cancellationToken);
        }

        public async Task<IReadOnlyList<EducationService>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.EducationServices.AsNoTracking().OrderBy(x => x.ServiceName).ToListAsync(cancellationToken);
        }

        public async Task<EducationService?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.EducationServices.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<EducationService?> GetByServiceNameAsync(string ServiceName, CancellationToken cancellationToken)
        {
            return await _dbContext.EducationServices.FirstOrDefaultAsync(x => x.ServiceName == ServiceName, cancellationToken);

        }
    }

}
