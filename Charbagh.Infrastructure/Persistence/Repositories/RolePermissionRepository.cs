using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Persistence.Repositories;

public sealed class RolePermissionRepository : IRolePermissionRepository
{
    private readonly CharbaghDbContext _context;

    public RolePermissionRepository(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task<bool> ExistsAsync( Guid roleId, Guid permissionId, CancellationToken cancellationToken)
    {
        return await _context.RolePermissions .AnyAsync( x => x.RoleId == roleId && x.PermissionId == permissionId,cancellationToken);
    }

    public async Task AddAsync( RolePermission rolePermission,CancellationToken cancellationToken)
    {
        await _context.RolePermissions.AddAsync( rolePermission,cancellationToken);
    }

    public async Task RemoveAsync(Guid roleId,Guid permissionId, CancellationToken cancellationToken)
    {
        var rolePermission = await _context.RolePermissions.FirstOrDefaultAsync( x => x.RoleId == roleId && x.PermissionId == permissionId, cancellationToken);

        if (rolePermission is not null)
        {
            _context.RolePermissions.Remove(rolePermission);
        }
    }
}