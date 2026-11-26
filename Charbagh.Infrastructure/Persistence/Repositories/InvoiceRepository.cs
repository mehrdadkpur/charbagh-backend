
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Finance;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class InvoiceRepository : IInvoiceRepository
    {
        private readonly CharbaghDbContext _charbaghDbContext;

        public InvoiceRepository(CharbaghDbContext charbaghDbContext)
        {
            _charbaghDbContext = charbaghDbContext;
        }

        public async Task AddAsync(Invoice invoice, CancellationToken cancellationToken)
        {
            await _charbaghDbContext.Invoices.AddAsync(invoice, cancellationToken);
        }

        public async Task<IReadOnlyList<Invoice>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.Invoices.AsNoTracking().Include(x => x.Items).Include(x => x.Discount).OrderByDescending(x => x.InvoiceDate).ToListAsync(cancellationToken);
        }

        public async Task<Invoice?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.Invoices.Include(x => x.Items).Include(x => x.Discount).FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<IReadOnlyList<Invoice>> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.Invoices
                .AsNoTracking()
                .Include(x => x.Items)
                .Include(x => x.Discount)
                .Where(x => x.StudentId == StudentId)
                .OrderByDescending(x => x.InvoiceDate)
                .ToListAsync(cancellationToken);
        }

        public async Task<Invoice?> GetForUpdateAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _charbaghDbContext.Invoices
                .Include(x => x.Items)
                .Include(x => x.Discount)
                .Include(x => x.Payments)
                .FirstOrDefaultAsync(
                    x => x.Id == id,
                    cancellationToken);
        }

    }
}
