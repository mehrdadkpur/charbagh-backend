using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class UserBranchRepository : IUserBranchRepository
    {
        private readonly CharbaghDbContext _context;

        public UserBranchRepository(CharbaghDbContext context)
        {
            _context = context;
        }
        public async Task AddAsync(UserBranch userBranch, CancellationToken cancellationToken)
        {
            await _context.UserBranches.AddAsync(userBranch , cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid userId, Guid branchId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.AnyAsync(x => x.UserId == userId && x.BranchId == branchId, cancellationToken);
        }

        public Task<bool> ExistsIsDefaultAsync(CancellationToken cancellationToken)
        {
            return _context.UserBranches.AnyAsync(x => !x.IsDefault, cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranch>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.UserBranches.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranch>> GetByAssigner(Guid assignerId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.AsNoTracking().Where(x => x.AssignedBy == assignerId).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranch>> GetByBranchIdAsync(Guid branchId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.AsNoTracking().Where(x => x.BranchId == branchId).ToListAsync(cancellationToken);
        }

        public async Task<UserBranch?> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.FirstOrDefaultAsync(x => x.Id == id, cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranch>> GetByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.AsNoTracking().Where(x => x.UserId == userId).ToListAsync(cancellationToken);

        }

        public async Task<UserBranch?> GetDefaultAsync(Guid userId, Guid branchId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.FirstOrDefaultAsync(x=> x.UserId == userId && x.BranchId == branchId , cancellationToken);
        }

        public async Task<UserBranch?> GetDefaultByUserIdAsync(Guid userId, CancellationToken cancellationToken)
        {
            return await _context.UserBranches.FirstOrDefaultAsync(
                x => x.UserId == userId && x.IsDefault && x.IsActive,
                cancellationToken);
        }
    }
}
