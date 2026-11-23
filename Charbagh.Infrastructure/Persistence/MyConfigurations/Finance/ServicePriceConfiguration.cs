using Charbagh.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Finance;

public sealed class ServicePriceConfiguration : IEntityTypeConfiguration<ServicePrice>
{
    public void Configure(EntityTypeBuilder<ServicePrice> builder)
    {
        builder.ToTable("ServicePrices");

        builder.HasKey(x => x.Id);

       builder.HasOne(x => x.EducationService)
            .WithMany(x => x.ServicePrices)
            .HasForeignKey(x => x.EducationServiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.EducationServiceId);

    }
}