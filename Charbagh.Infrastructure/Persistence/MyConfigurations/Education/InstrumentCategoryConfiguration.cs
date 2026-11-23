using Charbagh.Domain.Entities.Education.EducationCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations;

public sealed class InstrumentCategoryConfiguration
    : IEntityTypeConfiguration<InstrumentCategory>
{
    public void Configure(EntityTypeBuilder<InstrumentCategory> builder)
    {
        builder.ToTable("InstrumentCategories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Name)
            .HasMaxLength(100)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => x.Name)
            .IsUnique();

        builder.HasMany(x => x.Instruments)
            .WithOne(x => x.InstrumentCategory)
            .HasForeignKey(x => x.InstrumentCategoryId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}