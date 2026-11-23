using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class TeachingPlanConfiguration
    : IEntityTypeConfiguration<TeachingPlan>
{
    public void Configure(EntityTypeBuilder<TeachingPlan> builder)
    {
        builder.ToTable("TeachingPlans");

        builder.HasKey(x => x.Id);


        // LessonAssignment

        builder.HasOne(x => x.LessonAssignment)
            .WithMany(x => x.TeachingPlans)
            .HasForeignKey(x => x.LessonAssignmentId)
            .OnDelete(DeleteBehavior.Restrict);


        // Book

        builder.HasOne(x => x.Book)
            .WithMany(x => x.TeachingPlans)
            .HasForeignKey(x => x.BookId)
            .OnDelete(DeleteBehavior.Restrict);


        // StartBookItem

        builder.HasOne(x => x.StartBookItem)
            .WithMany()
            .HasForeignKey(x => x.StartBookItemId)
            .OnDelete(DeleteBehavior.Restrict);


        // TargetBookItem

        builder.HasOne(x => x.TargetBookItem)
            .WithMany()
            .HasForeignKey(x => x.TargetBookItemId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}