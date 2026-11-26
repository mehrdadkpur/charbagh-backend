using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class PaymentRepository : IPaymentRepository
{
    private readonly CharbaghDbContext _context;

    public PaymentRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken)
    {
        await _context.Payments.AddAsync(
            payment,
            cancellationToken);
    }

    public async Task<Payment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .AsNoTracking()
            .Include(x => x.Invoice)
                .ThenInclude(x => x.Items)
            .Include(x => x.Invoice)
                .ThenInclude(x => x.Discount)
            .Include(x => x.PaymentMethod)
            .Include(x => x.PaidByUser)
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<Payment?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .AsNoTracking()
            .Include(x => x.PaymentMethod)
            .OrderByDescending(x => x.PaymentDate)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }

    public async Task<IReadOnlyList<Payment>> GetByInvoiceIdAsync(
        Guid invoiceId,
        CancellationToken cancellationToken)
    {
        return await _context.Payments
            .AsNoTracking()
            .Include(x => x.PaymentMethod)
            .Include(x => x.PaidByUser)
            .Where(x => x.InvoiceId == invoiceId)
            .OrderByDescending(x => x.PaymentDate)
            .ThenByDescending(x => x.CreatedAt)
            .ToListAsync(cancellationToken);
    }
}