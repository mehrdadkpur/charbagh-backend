using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Setup;

public sealed class PermissionSeeder
{
    private readonly CharbaghDbContext _context;

    public PermissionSeeder(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync( CancellationToken cancellationToken = default)
    {
        var existingNames = await _context
            .Set<Permission>()
            .Select(permission => permission.Name)
            .ToListAsync(cancellationToken);

        var existingKeys = existingNames
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var definition in PermissionDefinitions.All)
        {
            var name = definition.Name.Trim();

            if (!existingKeys.Add(name))
                continue;

            var permission = Permission.Create(
                module: definition.Module,
                name: name,
                action: definition.Action,
                displayName: definition.DisplayName,
                description: definition.Description,
                isSystem: true);

            _context.Set<Permission>().Add(permission);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}
