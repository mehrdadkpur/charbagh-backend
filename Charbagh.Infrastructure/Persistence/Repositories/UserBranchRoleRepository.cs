
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class UserBranchRoleRepository : IUserBranchRoleRepository
    {
        private readonly CharbaghDbContext _context;

        public UserBranchRoleRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(UserBranchRole userBranchRole, CancellationToken cancellationToken)
        {
            await _context.UserBranchRoles.AddAsync(userBranchRole, cancellationToken);
        }

        public async Task<bool> ExistsAsync(Guid userBranchId, Guid roleId, CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.AnyAsync(x => x.UserBranchId ==  userBranchId && roleId == x.RoleId, cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranchRole>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<UserBranchRole?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.FirstOrDefaultAsync(x =>  x.Id == Id, cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranchRole>> GetByIsActiveAsync(bool isActive, CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.AsNoTracking().Where(x => x.IsActive == isActive).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranchRole>> GetByRoleIdAsync(Guid roleId, CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.AsNoTracking().Where(x => x.RoleId == roleId).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<UserBranchRole>> GetByUserBranchIdAsync(Guid userBranchId, CancellationToken cancellationToken)
        {
            return await _context.UserBranchRoles.AsNoTracking().Where(x => x.UserBranchId == userBranchId).ToListAsync(cancellationToken);
        }
    }
}
