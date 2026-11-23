using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories
{
    public class PermissionRepository : IPermissionRepository
    {
        private readonly CharbaghDbContext _context;

        public PermissionRepository(CharbaghDbContext charbaghDbContext)
        {
            _context = charbaghDbContext;
        }

        public async Task AddAsync(Permission permission, CancellationToken cancellationToken)
        {
           await _context.Permissions.AddAsync(permission, cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetAllAsync(CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().ToListAsync(cancellationToken);
        }

        public async Task<Permission?> GetByIdAsync(Guid Id, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().FirstOrDefaultAsync(x => x.Id == Id, cancellationToken);
        }

        public async Task<bool> ExistsAsync( string module, PermissionAction action, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AnyAsync( x => x.Module == module && x.Action == action, cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetByIsSystemPermissionAsync(bool IsSystemPermission, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().Where(x =>x.IsSystem == IsSystemPermission).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetByModuleAsync(string ModuleName, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().Where(x => x.Module == ModuleName).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetByPermissionActionAsync(PermissionAction PermissionAction, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().Where(x => x.Action == PermissionAction).ToListAsync(cancellationToken);
        }

        public async Task<IReadOnlyList<Permission>> GetByPermissionNameAsync(string PermissionName, CancellationToken cancellationToken)
        {
            return await _context.Permissions.AsNoTracking().Where(x => x.Name == PermissionName).ToListAsync(cancellationToken);
        }
    }
}
