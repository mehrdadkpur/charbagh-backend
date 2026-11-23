
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.People;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class StaffRepository : IStaffRepository
    {
        private readonly CharbaghDbContext _context;

        public StaffRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(Staff staff, CancellationToken cancellationToken)
        {
            await _context.Staffs.AddAsync(staff , cancellationToken);
        }

        public async Task<bool> ExistsByPersonnelCodeAsync(string personnelCode, CancellationToken cancellationToken)
        {
            return await _context.Staffs.AnyAsync(x => x.PersonnelCode == personnelCode, cancellationToken);
        }

        public Task<bool> ExistsByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return _context.Staffs.AnyAsync( x => x.UserId == userId, cancellationToken);
        }

        public async Task<IReadOnlyList<Staff>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Staffs
                .AsNoTracking()
                .Include(x =>x.User)
                .ToListAsync(cancellationToken);
        }

        public async Task<Staff?> GetByIdAsync(Guid StaffId, CancellationToken cancellationToken)
        {
            return await _context.Staffs
                .Include(x => x.User)
                .FirstOrDefaultAsync(x => x.Id == StaffId, cancellationToken);
        }

        public async Task<Staff?> GetbyNationalIdentityNumberAsync(string NationalIdentityNumber, CancellationToken cancellationToken)
        {
            return await _context.Staffs.FirstOrDefaultAsync(x => x.User.NationalIdentificationNumber == NationalIdentityNumber, cancellationToken);
        }

        public async Task<int> CountAsync(CancellationToken cancellationToken)
        {
            return await _context.Staffs.CountAsync(cancellationToken);
        }
    }
}
