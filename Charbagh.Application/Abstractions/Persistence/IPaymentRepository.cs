using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IPaymentRepository
{
    Task AddAsync(
        Payment payment,
        CancellationToken cancellationToken);

    Task<Payment?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Payment?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Payment>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Payment>> GetByInvoiceIdAsync(
        Guid invoiceId,
        CancellationToken cancellationToken);
}