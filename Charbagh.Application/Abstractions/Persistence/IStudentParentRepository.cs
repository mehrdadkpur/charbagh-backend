

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IStudentParentRepository
    {
        Task AddAsync (StudentParent studentParent , CancellationToken cancellationToken);

        Task<StudentParent?> GetByIdAsync (Guid StudentParentId , CancellationToken cancellationToken);

        Task <StudentParent?> GetByParentIdAsync (Guid ParentId , CancellationToken cancellationToken);

        Task<StudentParent?> GetByStudentIdAsync(Guid StudentId, CancellationToken cancellationToken);

        Task<bool> ExistsByStudentAndParentAsync(Guid studentId, Guid parentId, CancellationToken cancellationToken);

        Task<bool> ExistsByStudentId (Guid StudentId , CancellationToken cancellationToken);

        Task <bool> ExistsByParentId (Guid ParentId , CancellationToken cancellationToken);

        Task <IReadOnlyList<StudentParent>> GetAllAsync (CancellationToken cancellationToken);

        Task<StudentParent?> RemoveAsync(Guid Id , CancellationToken cancellationToken);
    }
}
