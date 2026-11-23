
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.CMS;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class ContactRepository : IContactRepository
    {

        private readonly CharbaghDbContext _context;

        public ContactRepository(CharbaghDbContext context)
        {
            _context = context;
        }

        public async Task AddAsync(Contact contact, CancellationToken cancellationToken)
        {
            await _context.Contacts.AddAsync(contact , cancellationToken);
        }

        public async Task<IReadOnlyList<Contact>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Contacts.AsNoTracking().OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<Contact?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Contacts.FirstOrDefaultAsync(x => x.Id == Id , cancellationToken);
        }

        public async Task<IReadOnlyList<Contact>> GetByMobileAsync(string Mobile, CancellationToken cancellationToken)
        {
            return await _context.Contacts.AsNoTracking().Where(x => x.Mobile == Mobile).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Contact>> GetByStatusAsync(ContactStatus Status, CancellationToken cancellationToken)
        {
            return await _context.Contacts.AsNoTracking().Where(x => x.Status == Status).OrderByDescending(x => x.CreatedAt).ToListAsync(cancellationToken);
        }
    }
}
