using Charbagh.Domain.Entities.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.MyConfigurations.Identity;

public sealed class UserConfiguration : IEntityTypeConfiguration<User>
{
    public void Configure(EntityTypeBuilder<User> builder)
    {
        builder.HasIndex(x => x.NationalIdentificationNumber)
            .IsUnique();
           
        builder.Property(x => x.SecurityStamp)
            .IsRequired()
            .HasMaxLength(100);

        builder.ToTable("Users")
        .HasOne(x => x.Nationality)
        .WithMany(x => x.Users)
        .HasForeignKey(x => x.NationalityId)
        .OnDelete(DeleteBehavior.Restrict);

    }
}