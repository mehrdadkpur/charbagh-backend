using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IInstrumentRepository
    {
        Task AddAsync(Domain.Entities.Education.EducationCatalog.Instrument instrument, CancellationToken cancellationToken);

        Task<Instrument?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

        Task<List<Instrument>> GetAllAsync( CancellationToken cancellationToken);

        Task<Instrument?> GetByNameAsync(Guid instrumentCategoryId, string name, CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
