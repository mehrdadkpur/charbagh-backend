using Charbagh.Domain.Entities.Identity;
using Charbagh.Infrastructure.Persistence.Context;
using Microsoft.EntityFrameworkCore;

namespace Charbagh.Infrastructure.Setup;

public sealed class NationalitySeeder
{
    private readonly CharbaghDbContext _context;

    public NationalitySeeder(CharbaghDbContext context)
    {
        _context = context;
    }

    public async Task SeedAsync(
        CancellationToken cancellationToken = default)
    {
        var definitions = new[]
        {
            (EnglishName: "Iran", PersianName: "ایران", Code: "IRN"),
            (EnglishName: "Afghanistan", PersianName: "افغانستان", Code: "AFG"),
            (EnglishName: "Azerbaijan", PersianName: "آذربایجان", Code: "AZE"),
            (EnglishName: "Turkey", PersianName: "ترکیه", Code: "TUR"),
            (EnglishName: "Iraq", PersianName: "عراق", Code: "IRQ"),
            (EnglishName: "Germany", PersianName: "آلمان", Code: "DEU")
        };

        var existingCodes = await _context.Set<Nationality>()
            .Select(n => n.Code)
            .ToListAsync(cancellationToken);

        var codeSet = existingCodes
            .ToHashSet(StringComparer.OrdinalIgnoreCase);

        foreach (var item in definitions)
        {
            if (!codeSet.Add(item.Code))
                continue;

            var nationality = Nationality.Create(
                englishCountryName: item.EnglishName,
                persianCountryName: item.PersianName,
                code: item.Code);

            _context.Set<Nationality>().Add(nationality);
        }

        await _context.SaveChangesAsync(cancellationToken);
    }
}