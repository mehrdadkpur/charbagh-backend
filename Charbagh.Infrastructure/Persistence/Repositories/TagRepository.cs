
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class TagRepository : ITagRepository
    {
        private readonly CharbaghDbContext _dbContext;

        public TagRepository(CharbaghDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public async Task AddAsync(Tag tag, CancellationToken cancellationToken)
        {
            await _dbContext.Tags.AddAsync(tag, cancellationToken);
        }

        public async Task<bool> ExistsTagNameAsync(string TagName, CancellationToken cancellationToken)
        {
            return await _dbContext.Tags.AnyAsync(x => x.Name == TagName , cancellationToken);
        }

        public async Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _dbContext.Tags.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Tag?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<Tag?> GetByTagNameAsync(string TagName, CancellationToken cancellationToken)
        {
            return await _dbContext.Tags.FirstOrDefaultAsync(x => x.Name == TagName , cancellationToken);
        }
    }
}
