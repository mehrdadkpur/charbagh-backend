using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IDiscountRepository
{
    Task AddAsync(
        Discount discount,
        CancellationToken cancellationToken);

    Task<Discount?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Discount?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Discount?> GetByInvoiceIdAsync(
        Guid invoiceId,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Discount>> GetAllAsync(
        CancellationToken cancellationToken);
}