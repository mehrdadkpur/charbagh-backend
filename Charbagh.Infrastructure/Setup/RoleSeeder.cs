using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Setup;

public sealed class RoleSeeder
{
    private const string SuperAdminRoleName = "SuperAdmin";

    private readonly CharbaghDbContext _context;

    public RoleSeeder(CharbaghDbContext context)
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
            role = Role.Create(
                name: SuperAdminRoleName,
                displayName: "مدیر کل سیستم",
                description: "System administrator",
                isSystem: true);

            _context.Set<Role>().Add(role);

            await _context.SaveChangesAsync(cancellationToken);
            return;
        }

        if (!role.IsSystem)
        {
            throw new InvalidOperationException(
                "The existing SuperAdmin role is not a system role.");
        }

        if (!role.IsActive)
        {
            role.Activate();

            await _context.SaveChangesAsync(cancellationToken);
        }
    }
}
