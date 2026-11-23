using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class LesssonSessionConfiguration
    : IEntityTypeConfiguration<LessonSession>
{
    public void Configure(EntityTypeBuilder<LessonSession> builder)
    {
        builder.ToTable("LessonSessions");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.LessonAssignment)
            .WithMany(x => x.LessonSessions)
            .HasForeignKey(x => x.LessonAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.LessonContents)
            .WithOne(x => x.LessonSession)
            .HasForeignKey(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasMany(x => x.PracticeAssignments)
            .WithOne(x=> x.LessonSession)
            .HasForeignKey(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // LessonSession → Evaluation
        builder.HasOne(x => x.Evaluation) 
            .WithOne(x => x.LessonSession) 
            .HasForeignKey<Evaluation>(x => x.LessonSessionId) 
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Attendance)
            .WithOne(x => x.LessonSession)
            .HasForeignKey<Attendance>(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(x => x.LessonAssignmentId); 
        builder.HasIndex(x => x.SessionDate);
    }
}