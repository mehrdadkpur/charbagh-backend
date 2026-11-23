using Charbagh.Domain.Entities.People;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations.People;

public sealed class TeacherInstrumentConfiguration
    : IEntityTypeConfiguration<TeacherInstrument>
{
    public void Configure(
        EntityTypeBuilder<TeacherInstrument> builder)
    {
        builder.ToTable("TeacherInstruments");

        builder.HasKey(x => x.Id);

        builder.Property(x => x.TeachingLevel)
            .IsRequired();

        builder.Property(x => x.DefaultSessionFee)
            .HasPrecision(18, 2);

        builder.Property(x => x.IsActive)
            .IsRequired();

        builder.HasIndex(x => new
        {
            x.TeacherId,
            x.InstrumentId
        })
        .IsUnique();

        builder.HasIndex(x => x.TeacherId);

        builder.HasIndex(x => x.InstrumentId);

        builder.HasOne(x => x.Teacher)
            .WithMany(x => x.TeacherInstruments)
            .HasForeignKey(x => x.TeacherId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.Instrument)
            .WithMany()
            .HasForeignKey(x => x.InstrumentId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasOne(x => x.DefaultBook)
            .WithMany()
            .HasForeignKey(x => x.DefaultBookId)
            .OnDelete(DeleteBehavior.Restrict);
    }
}