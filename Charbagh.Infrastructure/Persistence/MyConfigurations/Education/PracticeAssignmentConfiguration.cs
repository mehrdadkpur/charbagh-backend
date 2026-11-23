using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class PracticeAssignmentConfiguration : IEntityTypeConfiguration<PracticeAssignment>
{
    public void Configure(EntityTypeBuilder<PracticeAssignment> builder)
    {
        builder.ToTable("PracticeAssignments");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.LessonSession)
            .WithMany(x => x.PracticeAssignments)
            .HasForeignKey(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BookItem)
            .WithMany(x => x.PracticeAssignments)
            .HasForeignKey(x => x.BookItemId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.BookItemId);
    }
}