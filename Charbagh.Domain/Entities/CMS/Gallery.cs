using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.CMS;

public sealed class Gallery : BaseEntity
{
    public string Title { get; private set; } = null!;

    public string? Description { get; private set; }

    public string? CoverImageUrl { get; private set; }

    public GalleryStatus Status { get; private set; }

    public ICollection<GalleryItem> Items { get; private set; } = new List<GalleryItem>();

    private Gallery()
    {
    }

    private Gallery(
        string title,
        string? description,
        string? coverImageUrl)
    {
        Title = title;
        Description = description;
        CoverImageUrl = coverImageUrl;
        Status = GalleryStatus.Draft;
    }

    public static Gallery Create(
        string title,
        string? description = null,
        string? coverImageUrl = null)
    {
        Validate(title);

        var gallery = new Gallery(
            title.Trim(),
            description?.Trim(),
            coverImageUrl?.Trim());

        gallery.MarkAsCreated();

        return gallery;
    }

    public void Update(
        string title,
        string? description,
        string? coverImageUrl)
    {
        Validate(title);

        if (Status == GalleryStatus.Published)
            throw new InvalidOperationException(
                "Published galleries cannot be updated.");

        if (Status == GalleryStatus.Archived)
            throw new InvalidOperationException(
                "Archived galleries cannot be updated.");

        Title = title.Trim();
        Description = description?.Trim();
        CoverImageUrl = coverImageUrl?.Trim();

        MarkAsUpdated();
    }

    public void Publish()
    {
        if (Status == GalleryStatus.Published)
            return;

        if (Status == GalleryStatus.Archived)
            throw new InvalidOperationException(
                "Archived galleries cannot be published.");

        Status = GalleryStatus.Published;

        MarkAsUpdated();
    }

    public void Unpublish()
    {
        if (Status != GalleryStatus.Published)
            return;

        Status = GalleryStatus.Draft;

        MarkAsUpdated();
    }

    public void Archive()
    {
        if (Status == GalleryStatus.Archived)
            return;

        Status = GalleryStatus.Archived;

        MarkAsUpdated();
    }

    public void Restore()
    {
        if (Status != GalleryStatus.Archived)
            return;

        Status = GalleryStatus.Draft;

        MarkAsUpdated();
    }

    private static void Validate(string title)
    {
        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Gallery title is required.",
                nameof(title));

        if (title.Trim().Length < 2)
            throw new ArgumentException(
                "Gallery title must be at least 2 characters.",
                nameof(title));

        if (title.Trim().Length > 150)
            throw new ArgumentException(
                "Gallery title cannot be longer than 150 characters.",
                nameof(title));
    }

    public bool IsDraft =>
        Status == GalleryStatus.Draft;

    public bool IsPublished =>
        Status == GalleryStatus.Published;

    public bool IsArchived =>
        Status == GalleryStatus.Archived;
}