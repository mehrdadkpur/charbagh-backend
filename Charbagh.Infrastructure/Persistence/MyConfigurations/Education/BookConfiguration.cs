using Charbagh.Domain.Entities.Education.EducationCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.MyConfigurations.EducationCatalog
{
    public sealed class BookConfiguration : IEntityTypeConfiguration<Book>
    {
        public void Configure(EntityTypeBuilder<Book> builder)
        {
            builder.ToTable("Books");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.EnglishTitle)
                .HasMaxLength(100);

            builder.Property(x => x.Author)
                .HasMaxLength(100);

            builder.Property(x => x.Publisher)
                .HasMaxLength(100);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.InstrumentId,
                x.Title,
                x.Volume
            }).IsUnique();

            builder.HasOne(x => x.Instrument)
                .WithMany(x => x.Books)
                .HasForeignKey(x => x.InstrumentId)
                .OnDelete(DeleteBehavior.Restrict);

            builder.HasMany(x => x.BookItems)
                .WithOne(x => x.Book)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);
        }
    }
}