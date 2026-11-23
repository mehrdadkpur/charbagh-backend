using Charbagh.Domain.Entities.Education.EducationCatalog;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.MyConfigurations.EducationCatalog
{
    public sealed class BookItemConfiguration : IEntityTypeConfiguration<BookItem>
    {
        public void Configure(EntityTypeBuilder<BookItem> builder)
        {
            builder.ToTable("BookItems");

            builder.HasKey(x => x.Id);

            builder.Property(x => x.Title)
                .HasMaxLength(100)
                .IsRequired();

            builder.Property(x => x.Description)
                .HasMaxLength(500);

            builder.Property(x => x.IsActive)
                .IsRequired();

            builder.HasIndex(x => new
            {
                x.BookId,
                x.Title
            }).IsUnique();

            builder.HasOne(x => x.Book)
                .WithMany(x => x.BookItems)
                .HasForeignKey(x => x.BookId)
                .OnDelete(DeleteBehavior.Restrict);

        }
    }
}