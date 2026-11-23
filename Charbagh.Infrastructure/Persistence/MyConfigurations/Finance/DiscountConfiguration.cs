using Charbagh.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Finance;

public sealed class DiscountConfiguration
    : IEntityTypeConfiguration<Discount>
{
    public void Configure(EntityTypeBuilder<Discount> builder)
    {
        builder.ToTable("Discounts");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.InvoiceId)
            .IsRequired();

        builder.Property(x => x.DiscountTypeId)
            .IsRequired();

        builder.Property(x => x.ValueType)
            .IsRequired();

        builder.Property(x => x.Value)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.Description)
            .HasMaxLength(500);

        // هر Invoice حداکثر یک Discount
        builder.HasIndex(x => x.InvoiceId)
            .IsUnique();

        builder.HasIndex(x => x.DiscountTypeId);

        builder.HasOne(x => x.Invoice)
            .WithOne(x => x.Discount)
            .HasForeignKey<Discount>(x => x.InvoiceId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DiscountType)
            .WithMany(x => x.Discounts)
            .HasForeignKey(x => x.DiscountTypeId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}