using Charbagh.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Identity;

public sealed class SecurityAuditConfiguration
    : IEntityTypeConfiguration<SecurityAudit>
{
    public void Configure(EntityTypeBuilder<SecurityAudit> builder)
    {
        builder.ToTable("SecurityAudits");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.Action)
            .IsRequired();

        builder.Property(x => x.Success)
            .IsRequired();

        builder.Property(x => x.Description)
            .HasMaxLength(1000);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.DeviceName)
            .HasMaxLength(200);

        builder.Property(x => x.Metadata)
            .HasColumnType("nvarchar(max)");

        builder.Property(x => x.CreatedAt)
            .IsRequired();

        builder.Property(x => x.UpdatedAt);

        builder.HasOne(x => x.User)
            .WithMany()
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.Action);

        builder.HasIndex(x => x.CreatedAt);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.Action,
            x.CreatedAt
        });
    }
}