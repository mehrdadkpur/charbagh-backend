
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IInvoiceRepository
    {
        Task AddAsync(Invoice invoice, CancellationToken cancellationToken);

        Task<Invoice?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<Invoice>> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken);

        Task<Invoice?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken);

    }
}
