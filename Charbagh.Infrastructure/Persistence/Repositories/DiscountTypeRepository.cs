using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class DiscountTypeRepository: IDiscountTypeRepository
{
    private readonly CharbaghDbContext _context;

    public DiscountTypeRepository(
        CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        DiscountType discountType,
        CancellationToken cancellationToken)
    {
        await _context.DiscountTypes.AddAsync(
            discountType,
            cancellationToken);
    }

    public async Task<DiscountType?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.DiscountTypes
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<DiscountType?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.DiscountTypes
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<DiscountType>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.DiscountTypes
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return await _context.DiscountTypes
            .AnyAsync(
                x => x.Name == name,
                cancellationToken);
    }
}