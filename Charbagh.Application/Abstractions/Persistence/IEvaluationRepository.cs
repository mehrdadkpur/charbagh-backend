
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IEvaluationRepository
    {
        Task AddAsync(Evaluation evaluation, CancellationToken cancellationToken);

        Task<Evaluation?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<Evaluation>> GetAllAsync(CancellationToken cancellationToken);

        Task<Evaluation?> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken);
    }

}
