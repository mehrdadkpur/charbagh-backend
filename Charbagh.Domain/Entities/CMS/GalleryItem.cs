using Charbagh.Domain.Enums;

namespace Charbagh.Domain.Entities.CMS;

public sealed class GalleryItem : BaseEntity
{
    public Guid GalleryId { get; private set; }

    public GalleryMediaType MediaType { get; private set; }

    public string? Title { get; private set; }

    public string? Description { get; private set; }

    public string FileUrl { get; private set; } = null!;

    public string? ThumbnailUrl { get; private set; }

    public int SortOrder { get; private set; }

    public Gallery Gallery { get; private set; } = null!;

    private GalleryItem()
    {
    }

    private GalleryItem(
        Guid galleryId,
        GalleryMediaType mediaType,
        string? title,
        string? description,
        string fileUrl,
        string? thumbnailUrl,
        int sortOrder)
    {
        GalleryId = galleryId;
        MediaType = mediaType;
        Title = title;
        Description = description;
        FileUrl = fileUrl;
        ThumbnailUrl = thumbnailUrl;
        SortOrder = sortOrder;
    }

    public static GalleryItem Create(
        Guid galleryId,
        GalleryMediaType mediaType,
        string fileUrl,
        string? title = null,
        string? description = null,
        string? thumbnailUrl = null,
        int sortOrder = 0)
    {
        if (galleryId == Guid.Empty)
            throw new ArgumentException(
                "Gallery is required.",
                nameof(galleryId));

        if (!Enum.IsDefined(mediaType))
            throw new ArgumentException(
                "Invalid gallery media type.",
                nameof(mediaType));

        if (string.IsNullOrWhiteSpace(fileUrl))
            throw new ArgumentException(
                "File URL is required.",
                nameof(fileUrl));

        if (sortOrder < 0)
            throw new ArgumentException(
                "Sort order cannot be negative.",
                nameof(sortOrder));

        var item = new GalleryItem(
            galleryId,
            mediaType,
            title?.Trim(),
            description?.Trim(),
            fileUrl.Trim(),
            thumbnailUrl?.Trim(),
            sortOrder);

        item.MarkAsCreated();

        return item;
    }

    public void Update(
        string? title,
        string? description,
        string? thumbnailUrl)
    {
        Title = title?.Trim();
        Description = description?.Trim();
        ThumbnailUrl = thumbnailUrl?.Trim();

        MarkAsUpdated();
    }

    public void ChangeSortOrder(int sortOrder)
    {
        if (sortOrder < 0)
            throw new ArgumentException(
                "Sort order cannot be negative.",
                nameof(sortOrder));

        if (SortOrder == sortOrder)
            return;

        SortOrder = sortOrder;

        MarkAsUpdated();
    }
}