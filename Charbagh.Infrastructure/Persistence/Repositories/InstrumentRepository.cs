using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

public sealed class InstrumentRepository : IInstrumentRepository
{
    private readonly CharbaghDbContext _context;

    public InstrumentRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync( Instrument instrument, CancellationToken cancellationToken)
    {
        await _context.Instruments.AddAsync( instrument, cancellationToken);
    }

    public Task<Instrument?> GetByIdAsync(Guid id,CancellationToken cancellationToken)
    {
        return _context.Instruments
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public Task<List<Instrument>> GetAllAsync( CancellationToken cancellationToken)
    {
        return _context.Instruments
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public Task<Instrument?> GetByNameAsync( Guid instrumentCategoryId, string name, CancellationToken cancellationToken)
    {
        return _context.Instruments
                .FirstOrDefaultAsync(x => x.InstrumentCategoryId == instrumentCategoryId && x.Name == name,cancellationToken);
    }

    public async Task<int> CountAsync(CancellationToken cancellationToken)
    {
        return await _context.Instruments.CountAsync(cancellationToken);
    }

}