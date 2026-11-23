
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IInvoiceItemRepository
    {
        Task AddAsync(InvoiceItem invoiceItem , CancellationToken cancellationToken);

        Task<InvoiceItem?> GetByIdAsync(Guid Id , CancellationToken cancellationToken);

        Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<InvoiceItem>> GetByInvoiceIdAsync (Guid InvoiceId , CancellationToken cancellationToken);

    }

}
