using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class EvaluationConfiguration
    : IEntityTypeConfiguration<Evaluation>
{
    public void Configure(EntityTypeBuilder<Evaluation> builder)
    {
        builder.ToTable("Evaluations");

        builder.HasKey(x => x.Id);

        // LessonSession → Evaluation
        builder.HasOne(x => x.LessonSession)
            .WithOne(x => x.Evaluation)
            .HasForeignKey<Evaluation>(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        // User → Evaluation
        builder.HasOne(x => x.EvaluatedByUser)
            .WithMany()
            .HasForeignKey(x => x.EvaluatedByUserId)
            .OnDelete(DeleteBehavior.Restrict);

        // Indexes
        builder.HasIndex(x => x.LessonSessionId)
            .IsUnique();

        builder.HasIndex(x => x.EvaluatedByUserId);
    }
}