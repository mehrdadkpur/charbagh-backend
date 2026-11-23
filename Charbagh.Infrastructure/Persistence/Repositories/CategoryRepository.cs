
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class CategoryRepository : ICategoryRepository
    {
        private readonly CharbaghDbContext _dbContext;

        public CategoryRepository(CharbaghDbContext charbaghDbContext)
        {
            _dbContext = charbaghDbContext;
        }

        public async Task AddAsync(Category category, CancellationToken cancellationToken)
        {
            await _dbContext.Categories.AddAsync(category, cancellationToken);
        }

        public async Task<IReadOnlyList<Category>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Category?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Categories.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public Task<Category?> GetByNameAsync(string CategoryName, CancellationToken cancellationToken)
        {
            return _dbContext.Categories.FirstOrDefaultAsync(x => x.Name == CategoryName, cancellationToken);
        }
    }
}
