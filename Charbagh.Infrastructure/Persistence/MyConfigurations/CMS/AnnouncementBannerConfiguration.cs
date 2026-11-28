using Charbagh.Domain.Entities.CMS;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Charbagh.Infrastructure.Persistence.Configurations;

public sealed class AnnouncementBannerConfiguration : IEntityTypeConfiguration<AnnouncementBanner>
{
    public void Configure(EntityTypeBuilder<AnnouncementBanner> builder)
    {
        builder.ToTable("AnnouncementBanners");
        builder.HasKey(x => x.Id);
        builder.Property(x => x.Title).IsRequired().HasMaxLength(150);
        builder.Property(x => x.Description).HasMaxLength(500);
        builder.Property(x => x.ImageUrl).IsRequired().HasMaxLength(2048);
        builder.Property(x => x.LinkUrl).HasMaxLength(2048);
        builder.Property(x => x.ButtonText).HasMaxLength(60);
        builder.Property(x => x.Status).IsRequired();
        builder.HasIndex(x => new { x.Status, x.SortOrder });
        builder.HasIndex(x => new { x.StartsAt, x.EndsAt });
    }
}
