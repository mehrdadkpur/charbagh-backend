using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class TransactionRepository : ITransactionRepository
{
    private readonly CharbaghDbContext _context;

    public TransactionRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Transaction transaction,
        CancellationToken cancellationToken)
    {
        await _context.Transactions.AddAsync(
            transaction,
            cancellationToken);
    }

    public async Task<Transaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(x => x.Payment)
            .Include(x => x.CreatedByUser)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Transaction?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .Include(x => x.Payment)
                .ThenInclude(x => x.Invoice)
                    .ThenInclude(x => x.Items)
            .Include(x => x.Payment)
                .ThenInclude(x => x.Invoice)
                    .ThenInclude(x => x.Discount)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Include(x => x.Payment)
            .Include(x => x.CreatedByUser)
            .OrderByDescending(x => x.TransactionDate)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Transaction>> GetByPaymentIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken)
    {
        return await _context.Transactions
            .AsNoTracking()
            .Where(x => x.PaymentId == paymentId)
            .OrderByDescending(x => x.TransactionDate)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}