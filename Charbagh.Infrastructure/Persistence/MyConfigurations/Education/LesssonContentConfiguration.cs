using Charbagh.Domain.Entities.Education.StudentEducation;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.Education;

public sealed class LesssonContentConfiguration
    : IEntityTypeConfiguration<LessonContent>
{
    public void Configure(EntityTypeBuilder<LessonContent> builder)
    {
        builder.ToTable("LessonContents");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.LessonSession)
            .WithMany(x => x.LessonContents)
            .HasForeignKey(x => x.LessonSessionId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.BookItem)
            .WithMany()
            .HasForeignKey(x => x.BookItemId)
            .OnDelete(DeleteBehavior.Restrict);

        // Index
        builder.HasIndex(x => x.LessonSessionId); 
        builder.HasIndex(x => x.BookItemId);
    }
}