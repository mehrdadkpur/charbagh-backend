using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IContactRepository
    {
        Task AddAsync(Contact contact, CancellationToken cancellationToken);

        Task<Contact?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Contact>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList< Contact>> GetByMobileAsync(string Mobile, CancellationToken cancellationToken);

        Task<IReadOnlyList<Contact>> GetByStatusAsync(ContactStatus Status, CancellationToken cancellationToken);

    }
}
