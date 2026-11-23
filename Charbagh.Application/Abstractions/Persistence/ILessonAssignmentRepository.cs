using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ILessonAssignmentRepository
    {
        Task AddAsync (LessonAssignment lessonAssignment , CancellationToken cancellationToken);

        Task<LessonAssignment?> GetByIdAsync (Guid Id , CancellationToken cancellationToken);

        Task<LessonAssignment?> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken);

        Task<IReadOnlyList< LessonAssignment>> GetAllAsync ( CancellationToken cancellationToken);

        Task<bool> ExistsActiveAssignmentAsync( Guid studentId, Guid instrumentId, Guid? excludeLessonAssignmentId, CancellationToken cancellationToken);    
    }
}
