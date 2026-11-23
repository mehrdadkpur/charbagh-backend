using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class LessonAssignmentConfiguration
    : IEntityTypeConfiguration<LessonAssignment>
{
    public void Configure(
        EntityTypeBuilder<LessonAssignment> builder)
    {
        builder.ToTable("LessonAssignments");


        builder.HasKey(x => x.Id);


        builder.HasOne(x => x.Student)
            .WithMany(x => x.LessonAssignments)
            .HasForeignKey(x => x.StudentId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.LessonAssignments)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Instrument)
            .WithMany(x => x.LessonAssignments)
            .HasForeignKey(x => x.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.HasOne(x => x.Book)
            .WithMany()
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);


        builder.Property(x => x.SessionFee)
            .HasPrecision(18, 2);
    }
}