using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace Charbagh.Infrastructure.Persistence.Context;

public sealed class CharbaghDbContextFactory
    : IDesignTimeDbContextFactory<CharbaghDbContext>
{
    public CharbaghDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder =
            new DbContextOptionsBuilder<CharbaghDbContext>();

        optionsBuilder.UseSqlServer(
            "Server=.;Database=CharbaghDb;Trusted_Connection=True;TrustServerCertificate=True");

        return new CharbaghDbContext(
            optionsBuilder.Options);
    }
}