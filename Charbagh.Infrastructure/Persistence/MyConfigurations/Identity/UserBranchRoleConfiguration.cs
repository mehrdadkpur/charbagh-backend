using Charbagh.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Identity;

public sealed class UserBranchRoleConfiguration
    : IEntityTypeConfiguration<UserBranchRole>
{
    public void Configure(EntityTypeBuilder<UserBranchRole> builder)
    {
        builder.ToTable("UserBranchRoles");

        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.UserBranchId,
            x.RoleId
        })
            .IsUnique();

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.Property(x => x.AssignedAt)
            .IsRequired();

        builder.HasOne(x => x.UserBranch)
            .WithMany(x => x.UserBranchRoles)
            .HasForeignKey(x => x.UserBranchId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Role)
            .WithMany(x => x.UserBranchRoles)
            .HasForeignKey(x => x.RoleId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.AssignedByUser)
            .WithMany()
            .HasForeignKey(x => x.AssignedBy)
            .OnDelete(DeleteBehavior.Restrict);
    }
}