
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class RoleRepository : IRoleRepository
    {
        private readonly CharbaghDbContext _context;

        public RoleRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }
        public async Task AddAsync(Role role, CancellationToken cancellationToken)
        {
            await _context.Roles.AddAsync(role, cancellationToken);
        }

        public Task<bool> ExistsNameAsync(string name, Guid? excludeRoleId , CancellationToken cancellationToken)
        {
            return _context.Roles.AnyAsync(x => x.Name == name && x.Id != excludeRoleId, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Roles
                .AsNoTracking()
                .ToListAsync(cancellationToken);
        }

        public async Task<Role?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Roles.FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetByIsActiveAsync(bool IsActiveRole, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Where(x => x.IsActive == IsActiveRole).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetByIsSystemAsync(bool IsSystemRole, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Where(x => x.IsSystem == IsSystemRole).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Role>> GetByNameAsync(string roleName, CancellationToken cancellationToken)
        {
            return await _context.Roles.AsNoTracking().Where(x => x.Name == roleName).ToListAsync(cancellationToken);
        }

        public async Task<bool> HasPermissionAsync( Guid roleId, Guid permissionId, CancellationToken cancellationToken)
        {
            return await _context.RolePermissions.AnyAsync( x => x.RoleId == roleId && x.PermissionId == permissionId, cancellationToken);
        }
    }
}
