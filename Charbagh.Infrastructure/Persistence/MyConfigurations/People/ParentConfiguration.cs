using Charbagh.Domain.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Charbagh.Infrastructure.Persistence.MyConfigurations.People
{
    public sealed class ParentConfiguration : IEntityTypeConfiguration<Parent>
    {
        public void Configure(EntityTypeBuilder<Parent> builder)
        {
            builder.ToTable("Parents");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasOne(x => x.User)
               .WithOne(x => x.Parent)
               .HasForeignKey<Parent>(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
