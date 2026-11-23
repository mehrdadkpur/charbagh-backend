using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IPaymentMethodRepository
{
    Task AddAsync(
        PaymentMethod paymentMethod,
        CancellationToken cancellationToken);

    Task<PaymentMethod?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<PaymentMethod?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<PaymentMethod>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);
}