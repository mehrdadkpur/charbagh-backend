using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Setup;

public sealed class RolePermissionSeeder
{
    private const string SuperAdminRoleName = "SuperAdmin";

    private readonly CharbaghDbContext _context;

    public RolePermissionSeeder(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(
        CancellationToken cancellationToken = default)
    {
        var role = await _context.Set<Role>()
            .SingleOrDefaultAsync(
                x => x.Name == SuperAdminRoleName,
                cancellationToken);

        if (role is null)
        {
            throw new InvalidOperationException(
                "SuperAdmin role must be seeded before role permissions.");
        }

        if (!role.IsActive || !role.IsSystem)
        {
            throw new InvalidOperationException(
                "SuperAdmin must be an active system role.");
        }

        var permissionIds = await _context.Set<Permission>()
            .Select(x => x.Id)
            .ToListAsync(cancellationToken);

        var existingPermissionIds = await _context
            .Set<RolePermission>()
            .Where(x => x.RoleId == role.Id)
            .Select(x => x.PermissionId)
            .ToListAsync(cancellationToken);

        var existingIds = existingPermissionIds.ToHashSet();

        foreach (var permissionId in permissionIds)
        {
            if (!existingIds.Add(permissionId))
                continue;

            _context.Set<RolePermission>().Add(
                RolePermission.Create(
                    role.Id,
                    permissionId));
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
