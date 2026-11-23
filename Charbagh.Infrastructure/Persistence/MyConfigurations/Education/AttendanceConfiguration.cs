using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class AttendanceConfiguration : IEntityTypeConfiguration<Attendance>
{
    public void Configure(EntityTypeBuilder<Attendance> builder)
    {
        builder.ToTable("Attendances");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.LessonSession)
            .WithOne(x => x.Attendance)
            .HasForeignKey<Attendance>(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.RecordedByUser)
            .WithMany()
            .HasForeignKey(x => x.RecordedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.LessonSessionId)
            .IsUnique();

        builder.HasIndex(x => x.RecordedByUserId);
    }
}