using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.People;

public sealed class StudentParentConfiguration
    : IEntityTypeConfiguration<StudentParent>
{
    public void Configure(
        EntityTypeBuilder<StudentParent> builder)
    {
        builder.ToTable("StudentParents");


        builder.HasKey(x => x.Id);

        builder.HasIndex(x => new
        {
            x.StudentId,
            x.ParentId
        }).IsUnique();

        builder.HasOne(x => x.Student)
            .WithMany(x => x.StudentParents)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Parent)
            .WithMany(x => x.StudentParents)
            .HasForeignKey(x => x.ParentId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}