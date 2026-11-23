using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public sealed class NationalityRepository : INationalityRepository
    {
        private readonly CharbaghDbContext _dbContext;

        public NationalityRepository(CharbaghDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Nationality nationality, CancellationToken cancellationToken)
        {
            await _dbContext.Nationalities.AddAsync(nationality, cancellationToken);
        }

        public async Task<bool> ExistsByCodeAsync( string code, CancellationToken cancellationToken)
        {
            return await _dbContext.Nationalities.AnyAsync(x => x.Code == code, cancellationToken);
        }

        public async Task<bool> ExistsByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Nationalities.AnyAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<List<Nationality>> GetAllAsync(CancellationToken cancellationToken)
        {
           return await _dbContext.Nationalities.AsNoTracking().OrderBy(x => x.PersianCountryName).ToListAsync(cancellationToken);
        }

        public async Task<Nationality?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _dbContext.Nationalities.FirstOrDefaultAsync(x => x.Id == id);
        }
    }
}
