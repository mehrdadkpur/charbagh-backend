using Charbagh.Domain.Entities.Settings;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations;

public sealed class EducationServiceConfiguration : IEntityTypeConfiguration<EducationService>
{
    public void Configure(EntityTypeBuilder<EducationService> builder)
    {
        builder.ToTable("EducationServices");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.ServiceName)
            .IsRequired()
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasMany(x => x.ServicePrices)
            .WithOne(x => x.EducationService)
            .HasForeignKey(x => x.EducationServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.ServiceName)
            .IsUnique();
    }
}