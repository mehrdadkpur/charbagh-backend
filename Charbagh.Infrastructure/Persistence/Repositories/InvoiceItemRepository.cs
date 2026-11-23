using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class InvoiceItemRepository : IInvoiceItemRepository
    {

        private readonly CharbaghDbContext _context;

        public InvoiceItemRepository(CharbaghDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(InvoiceItem invoiceItem, CancellationToken cancellationToken)
        {
            await _context.InvoiceItems.AddAsync(invoiceItem, cancellationToken);
        }

        public async Task<IReadOnlyList<InvoiceItem>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.InvoiceItems.AsNoTracking().OrderBy(x => x.Quantity).ToListAsync(cancellationToken);
        }

        public async Task<InvoiceItem?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.InvoiceItems.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<IReadOnlyList< InvoiceItem>> GetByInvoiceIdAsync(Guid InvoiceId, CancellationToken cancellationToken)
        {
            return await _context.InvoiceItems.AsNoTracking().Where(x => x.InvoiceId == InvoiceId).OrderBy(x => x.Id).ToListAsync(cancellationToken);
        }
    }

}
