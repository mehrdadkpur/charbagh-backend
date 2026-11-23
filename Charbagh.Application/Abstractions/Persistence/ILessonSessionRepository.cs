
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ILessonSessionRepository
    {
        Task AddAsync(LessonSession lessonSession , CancellationToken cancellationToken);

        Task<LessonSession?> GetByIdAsync (Guid Id , CancellationToken cancellationToken);
        
        Task <IReadOnlyList<LessonSession>> GetAllAsync(CancellationToken cancellationToken);

        Task<IReadOnlyList<LessonSession>> GetByLessonAssignmentIdAsync(Guid lessonAssignmentId, CancellationToken cancellationToken);

        Task<bool> ExistsByLessonAssignmentAndDateAsync(Guid lessonAssignmentId,DateTime sessionDate,Guid? excludeLessonSessionId,CancellationToken cancellationToken);
    }

}
