
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IStudentRepository
    {
        Task AddAsync(Student student, CancellationToken cancellationToken);

        Task<bool> ExistsByUserIdAsync( Guid userId, CancellationToken cancellationToken);

        Task<bool> ExistsByStudentCodeAsync( string studentCode, CancellationToken cancellationToken);

        Task<bool> ExistsByUserIdExceptAsync(Guid userId, Guid? exceptStudentId, CancellationToken cancellationToken);

        Task<bool> ExistsByStudentCodeExceptAsync(string studentCode, Guid? exceptStudentId, CancellationToken cancellationToken);

        Task<Student?> GetByIdAsync( Guid studentId, CancellationToken cancellationToken);

        Task<IReadOnlyList<Student>> GetAllAsync( CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
