
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IAttendanceRepository
    {
        Task AddAsync(Attendance attendance , CancellationToken cancellationToken);

        Task<Attendance?> GetByIdAsync(Guid Id , CancellationToken cancellationToken);

        Task <IReadOnlyList< Attendance>> GetAllAsync (CancellationToken cancellationToken);

        Task<Attendance?> GetByLessonSessionIdAsync(Guid LessonSessionId, CancellationToken cancellationToken);
    }

}
