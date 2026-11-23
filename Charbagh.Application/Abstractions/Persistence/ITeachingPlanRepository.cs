
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ITeachingPlanRepository
    {
        Task AddAsync(TeachingPlan teachingPlan,CancellationToken cancellationToken);

        Task<TeachingPlan?> GetByIdAsync( Guid id, CancellationToken cancellationToken);

        Task<IReadOnlyList<TeachingPlan>> GetAllAsync( CancellationToken cancellationToken);

        Task<IReadOnlyList<TeachingPlan>> GetByLessonAssignmentIdAsync( Guid lessonAssignmentId, CancellationToken cancellationToken);

        Task<bool> ExistsActiveByLessonAssignmentIdAsync( Guid lessonAssignmentId, Guid? excludeTeachingPlanId, CancellationToken cancellationToken);

        Task<bool> ExistsByLessonAssignmentAndDisplayOrderAsync(Guid lessonAssignmentId,int displayOrder, Guid? excludeTeachingPlanId,CancellationToken cancellationToken);
    }
}
