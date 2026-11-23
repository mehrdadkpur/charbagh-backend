using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Infrastructure.Persistence.Context;

namespace Charbagh.Infrastructure.Persistence;

public sealed class UnitOfWork : IUnitOfWork
{
    private readonly CharbaghDbContext _context;

    public UnitOfWork(CharbaghDbContext context)
    {
        _context = context;
    }


    public async Task<int> SaveChangesAsync( CancellationToken cancellationToken = default)
    {
        return await _context.SaveChangesAsync(cancellationToken);
    }
}