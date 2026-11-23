using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence;

public interface ITransactionRepository
{
    Task AddAsync(
        Transaction transaction,
        CancellationToken cancellationToken);

    Task<Transaction?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Transaction?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Transaction>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Transaction>> GetByPaymentIdAsync(
        Guid paymentId,
        CancellationToken cancellationToken);
}