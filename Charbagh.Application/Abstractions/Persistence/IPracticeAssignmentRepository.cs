using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IPracticeAssignmentRepository
    {
        Task AddAsync(PracticeAssignment practiceAssignment, CancellationToken cancellationToken);

        Task<PracticeAssignment?> GetByIdAsync(Guid Id, CancellationToken cancellationToken);

        Task<IReadOnlyList<PracticeAssignment>> GetAllAsync(CancellationToken cancellationToken);

        Task<PracticeAssignment?> GetByLessonSessionIdAsync(Guid lessonSessionId, CancellationToken cancellationToken);
    }
}
