
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface ITeacherRepository
    {
        Task AddAsync(Teacher teacher, CancellationToken cancellationToken);

        Task<bool> ExistsByUserIdAsync( Guid userId, CancellationToken cancellationToken);

        Task<bool> ExistsByTeacherCodeAsync( string teacherCode, CancellationToken cancellationToken);

        Task<bool> ExistsByUserIdExceptAsync(Guid userId, Guid? exceptTeacherId, CancellationToken cancellationToken);

        Task<bool> ExistsByTeacherCodeExceptAsync(string teacherCode, Guid? exceptteacherId, CancellationToken cancellationToken);

        Task<Teacher?> GetByIdAsync( Guid teacherId, CancellationToken cancellationToken);

        Task<IReadOnlyList<Teacher>> GetAllAsync( CancellationToken cancellationToken);

        Task<int> CountAsync( CancellationToken cancellationToken);
    }
}
