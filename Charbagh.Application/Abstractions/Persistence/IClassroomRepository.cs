using Charbagh.Domain.Entities.Branches;

namespace Charbagh.Application.Abstractions.Persistence;

public interface IClassroomRepository
{
    Task AddAsync(
        Classroom classroom,
        CancellationToken cancellationToken);

    Task<Classroom?> GetByIdAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<Classroom?> GetForUpdateAsync(
        Guid id,
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Classroom>> GetAllAsync(
        CancellationToken cancellationToken);

    Task<IReadOnlyList<Classroom>> GetByBranchIdAsync(
        Guid branchId,
        CancellationToken cancellationToken);

    Task<bool> ExistsByCodeAsync(
        Guid branchId,
        string code,
        CancellationToken cancellationToken);
}