using Charbagh.Domain.Entities.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations;

public sealed class GalleryItemConfiguration : IEntityTypeConfiguration<GalleryItem>
{
    public void Configure(EntityTypeBuilder<GalleryItem> builder)
    {
        builder.ToTable("GalleryItems");

        builder.HasKey(x => x.Id);

        builder.HasOne(x => x.Gallery)
            .WithMany(x => x.Items)
            .HasForeignKey(x => x.GalleryId)
            .OnDelete(DeleteBehavior.Restrict);

        builder.HasIndex(x => x.Id);
    }
}