using Charbagh.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.MyConfigurations.Identity;

public sealed class NationalityConfiguration : IEntityTypeConfiguration<Nationality>
{
    public void Configure(EntityTypeBuilder<Nationality> builder)
    {
        builder.ToTable("Nationalities");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.EnglishCountryName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.PersianCountryName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Code)
            .IsRequired()
            .HasMaxLength(10);

        builder.HasIndex(x => x.Code)
            .IsUnique();

        builder.HasMany(x => x.Users)
            .WithOne(x => x.Nationality)
            .HasForeignKey(x => x.NationalityId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}