using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class PaymentMethodRepository
    : IPaymentMethodRepository
{
    private readonly CharbaghDbContext _context;

    public PaymentMethodRepository(
        CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task AddAsync(
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken)
    {
        await _context.PaymentMethods.AddAsync(
            paymentMethod,
            cancellationToken);
    }

    public async Task<PaymentMethod?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.PaymentMethods
            .AsNoTracking()
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<PaymentMethod?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken)
    {
        return await _context.PaymentMethods
            .FirstOrDefaultAsync(
                x => x.Id == id,
                cancellationToken);
    }

    public async Task<IReadOnlyList<PaymentMethod>> GetAllAsync(
        CancellationToken cancellationToken)
    {
        return await _context.PaymentMethods
            .AsNoTracking()
            .OrderBy(x => x.DisplayOrder)
            .ThenBy(x => x.Name)
            .ToListAsync(cancellationToken);
    }

    public async Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken)
    {
        return await _context.PaymentMethods
            .AnyAsync(
                x => x.Name == name,
                cancellationToken);
    }
}