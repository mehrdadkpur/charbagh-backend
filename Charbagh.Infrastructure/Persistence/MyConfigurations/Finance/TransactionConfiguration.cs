using Charbagh.Domain.Entities.Finance;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.MyConfigurations.Finance;

public sealed class TransactionConfiguration
    : IEntityTypeConfiguration<Transaction>
{
    public void Configure(EntityTypeBuilder<Transaction> builder)
    {
        builder.ToTable("Transactions");

        builder.HasKey(x => x.Id);


        // Foreign Keys

        builder.Property(x => x.PaymentId)
            .IsRequired();

        builder.Property(x => x.CreatedByUserId)
            .IsRequired();


        // Transaction Data

        builder.Property(x => x.Type)
            .IsRequired();

        builder.Property(x => x.Amount)
            .IsRequired()
            .HasPrecision(18, 2);

        builder.Property(x => x.TransactionDate)
            .IsRequired();

        builder.Property(x => x.Status)
            .IsRequired();

        builder.Property(x => x.ReferenceNumber)
            .HasMaxLength(100);

        builder.Property(x => x.Description)
            .HasMaxLength(500);


        // Indexes

        builder.HasIndex(x => x.PaymentId);

        builder.HasIndex(x => x.CreatedByUserId);

        builder.HasIndex(x => x.TransactionDate);

        builder.HasIndex(x => x.Status);


        // Relationships

        builder.HasOne(x => x.Payment)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.PaymentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.CreatedByUser)
            .WithMany(x => x.Transactions)
            .HasForeignKey(x => x.CreatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}