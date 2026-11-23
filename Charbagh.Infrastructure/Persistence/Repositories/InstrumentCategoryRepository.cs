using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class InstrumentCategoryRepository : IInstrumentCategoryRepository
{
    private readonly CharbaghDbContext _context;

    public InstrumentCategoryRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync( InstrumentCategory category, CancellationToken cancellationToken)
    {
        await _context.InstrumentCategories.AddAsync(category, cancellationToken);
    }

    public Task<InstrumentCategory?> GetByIdAsync( Guid id,CancellationToken cancellationToken)
    {
        return _context.InstrumentCategories
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task<List<InstrumentCategory>> GetAllAsync(CancellationToken cancellationToken)
    {
        return _context.InstrumentCategories
            .OrderBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<InstrumentCategory?> GetByNameAsync( string name,CancellationToken cancellationToken)
    {
        return _context.InstrumentCategories
            .FirstOrDefaultAsync(
                x => x.Name == name,
                cancellationToken);
    }
}