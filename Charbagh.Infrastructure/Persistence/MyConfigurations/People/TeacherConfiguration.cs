using Charbagh.Domain.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace Charbagh.Infrastructure.Persistence.MyConfigurations.People
{
    public sealed class TeacherConfiguration : IEntityTypeConfiguration<Teacher>
    {
        public void Configure(EntityTypeBuilder<Teacher> builder)
        {
            builder.ToTable("Teachers");

            builder.HasKey(x => x.Id);

            builder.HasIndex(x => x.UserId)
                .IsUnique();

            builder.HasOne(x => x.User)
               .WithOne(x => x.Teacher)
               .HasForeignKey<Teacher>(x => x.UserId)
               .OnDelete(DeleteBehavior.Restrict);
            
        }
    }
}
