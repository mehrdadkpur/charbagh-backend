using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class DiscountRepository : IDiscountRepository
{
    private readonly CharbaghDbContext _context;

    public DiscountRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Discount discount,
        CancellationToken cancellationToken)
    {
        await _context.Discounts.AddAsync(
            discount,
            cancellationToken);
    }


    public async Task<Discount?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Discounts
            .AsNoTracking()
            .Include(x => x.Invoice)
            .Include(x => x.DiscountType)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }


    public async Task<Discount?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Discounts
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }


    public async Task<Discount?> GetByInvoiceIdAsync(
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        return await _context.Discounts
            .AsNoTracking()
            .Include(x => x.DiscountType)
            .FirstOrDefaultAsync(
                x => x.InvoiceId == invoiceId,
                cancellationToken);
    }


    public async Task<IReadOnlyList<Discount>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Discounts
            .AsNoTracking()
            .Include(x => x.DiscountType)
            .OrderByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}