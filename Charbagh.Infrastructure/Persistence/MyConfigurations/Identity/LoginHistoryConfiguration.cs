using Charbagh.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Identity;

public sealed class LoginHistoryConfiguration
    : IEntityTypeConfiguration<LoginHistory>
{
    public void Configure(EntityTypeBuilder<LoginHistory> builder)
    {
        builder.ToTable("LoginHistories");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.LoginAt)
            .IsRequired();

        builder.Property(x => x.IsSuccessful)
            .IsRequired();

        builder.Property(x => x.FailureReason)
            .HasMaxLength(500);

        builder.Property(x => x.IpAddress)
            .HasMaxLength(45);

        builder.Property(x => x.UserAgent)
            .HasMaxLength(500);

        builder.Property(x => x.DeviceName)
            .HasMaxLength(200);

        builder.HasOne(x => x.User)
            .WithMany(x => x.LoginHistories)
            .HasForeignKey(x => x.UserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.UserId);

        builder.HasIndex(x => x.LoginAt);

        builder.HasIndex(x => new
        {
            x.UserId,
            x.LoginAt
        });
    }
}