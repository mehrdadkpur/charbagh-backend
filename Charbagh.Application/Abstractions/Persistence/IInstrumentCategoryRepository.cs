using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IInstrumentCategoryRepository
{
    Task AddAsync(InstrumentCategory category, CancellationToken cancellationToken);

    Task<InstrumentCategory?> GetByIdAsync(Guid id, CancellationToken cancellationToken);

    Task<List<InstrumentCategory>> GetAllAsync( CancellationToken cancellationToken);

    Task<InstrumentCategory?> GetByNameAsync( string name, CancellationToken cancellationToken);
}