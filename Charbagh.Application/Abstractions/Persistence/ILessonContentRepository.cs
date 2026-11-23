
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ILessonContentRepository
    {
        Task AddAsync (LessonContent lessonContent , CancellationToken cancellationToken);

        Task <LessonContent?> GetByIdAsync(Guid Id , CancellationToken cancellationToken);

        Task <IReadOnlyList< LessonContent>> GetAllAsync (CancellationToken cancellationToken);

        Task<IReadOnlyList<LessonContent>> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken);
    }
}
