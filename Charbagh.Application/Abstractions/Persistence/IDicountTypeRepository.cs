using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IDiscountTypeRepository
{
    Task AddAsync(
        DiscountType discountType,
        CancellationToken cancellationToken);

    Task<DiscountType?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<DiscountType?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<DiscountType>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<bool> ExistsByNameAsync(
        string name,
        CancellationToken cancellationToken);
}