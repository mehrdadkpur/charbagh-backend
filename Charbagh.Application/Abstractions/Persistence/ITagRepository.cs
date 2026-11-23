using Charbagh.Domain.Entities.CMS;
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ITagRepository
    {
        Task AddAsync(Tag tag, CancellationToken cancellationToken);

        Task<Tag?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Tag>> GetAllAsync(CancellationToken cancellationToken);

        Task<Tag?> GetByTagNameAsync(string TagName, CancellationToken cancellationToken);

        Task<bool> ExistsTagNameAsync (string TagName, CancellationToken cancellationToken); 
    }
}
