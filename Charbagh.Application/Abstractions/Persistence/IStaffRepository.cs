
using Charbagh.Domain.Entities.People;

namespace Charbagh.Application.Abstractions.Persistence
{
    public interface IStaffRepository
    {
        Task AddAsync(Staff staff , CancellationToken cancellationToken);

        Task<Staff?> GetByIdAsync(Guid StaffId , CancellationToken cancellationToken);

        Task<IReadOnlyList<Staff>> GetAllAsync (CancellationToken cancellationToken);

        Task <Staff?> GetbyNationalIdentityNumberAsync (string  NationalIdentityNumber, CancellationToken cancellationToken);

        Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken);

        Task<bool> ExistsByPersonnelCodeAsync(string personnelCode, CancellationToken cancellationToken);

        Task<int> CountAsync(CancellationToken cancellationToken);
    }
}
