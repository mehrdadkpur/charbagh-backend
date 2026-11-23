using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.People;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class ParentRepository : IParentRepository
    {
        private readonly CharbaghDbContext _dbContext;
        
        public ParentRepository(CharbaghDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task AddAsync(Parent parent, CancellationToken cancellationToken)
        {
           await _dbContext.Parents.AddAsync(parent, cancellationToken);
        }

        public async Task<IReadOnlyList<Parent>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Parents
                .AsNoTracking()
                .Include(x=> x.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<Parent?> GetByIdAsync(Guid ParentId, CancellationToken cancellationToken)
        {
            return await _dbContext.Parents
                .Include(x=> x.User)
                .FirstOrDefaultAsync(x => x.Id == ParentId , cancellationToken);
        }

        public async Task<bool> ExistsByUserIdAsync(Guid UserId, CancellationToken cancellationToken)
        {
            return await _dbContext.Parents.AnyAsync(x => x.UserId == UserId , cancellationToken);
        }
    }
}
