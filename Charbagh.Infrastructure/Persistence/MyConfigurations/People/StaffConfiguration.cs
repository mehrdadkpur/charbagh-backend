using Charbagh.Domain.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Charbagh.Infrastructure.Persistence.MyConfigurations.People
{
    public sealed class StaffConfiguration : IEntityTypeConfiguration<Staff>
    {
        public void Configure(EntityTypeBuilder<Staff> builder)
        {
            builder.ToTable("Staffs");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasOne(x => x.User)
               .WithOne(x => x.Staff)
               .HasForeignKey<Staff>(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
