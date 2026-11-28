namespace Charbagh.Domain.Entities.CMS;

public sealed class AnnouncementBanner : BaseEntity
{
    public string Title { get; private set; } = null!;
    public string? Description { get; private set; }
    public string ImageUrl { get; private set; } = null!;
    public string? LinkUrl { get; private set; }
    public string? ButtonText { get; private set; }
    public DateTime? StartsAt { get; private set; }
    public DateTime? EndsAt { get; private set; }
    public int SortOrder { get; private set; }
    public BannerStatus Status { get; private set; }

    private AnnouncementBanner() { }

    private AnnouncementBanner(
        string title,
        string? description,
        string imageUrl,
        string? linkUrl,
        string? buttonText,
        DateTime? startsAt,
        DateTime? endsAt,
        int sortOrder)
    {
        Title = title;
        Description = description;
        ImageUrl = imageUrl;
        LinkUrl = linkUrl;
        ButtonText = buttonText;
        StartsAt = NormalizeUtc(startsAt);
        EndsAt = NormalizeUtc(endsAt);
        SortOrder = sortOrder;
        Status = BannerStatus.Draft;
    }

    public static AnnouncementBanner Create(
        string title,
        string? description,
        string imageUrl,
        string? linkUrl,
        string? buttonText,
        DateTime? startsAt,
        DateTime? endsAt,
        int sortOrder)
    {
        Validate(title, imageUrl, linkUrl, startsAt, endsAt);
        var banner = new AnnouncementBanner(
            title.Trim(), description?.Trim(), imageUrl.Trim(),
            linkUrl?.Trim(), buttonText?.Trim(), startsAt, endsAt, sortOrder);
        banner.MarkAsCreated();
        return banner;
    }

    public void Update(
        string title,
        string? description,
        string imageUrl,
        string? linkUrl,
        string? buttonText,
        DateTime? startsAt,
        DateTime? endsAt,
        int sortOrder)
    {
        Validate(title, imageUrl, linkUrl, startsAt, endsAt);
        if (Status == BannerStatus.Archived)
            throw new InvalidOperationException("Archived banners cannot be updated.");

        Title = title.Trim();
        Description = description?.Trim();
        ImageUrl = imageUrl.Trim();
        LinkUrl = linkUrl?.Trim();
        ButtonText = buttonText?.Trim();
        StartsAt = NormalizeUtc(startsAt);
        EndsAt = NormalizeUtc(endsAt);
        SortOrder = sortOrder;
        MarkAsUpdated();
    }

    public void Publish()
    {
        if (Status == BannerStatus.Archived)
            throw new InvalidOperationException("Archived banners cannot be published.");
        Status = BannerStatus.Published;
        MarkAsUpdated();
    }

    public void Unpublish()
    {
        if (Status != BannerStatus.Published) return;
        Status = BannerStatus.Draft;
        MarkAsUpdated();
    }

    public void Archive()
    {
        Status = BannerStatus.Archived;
        MarkAsUpdated();
    }

    public void Restore()
    {
        if (Status != BannerStatus.Archived) return;
        Status = BannerStatus.Draft;
        MarkAsUpdated();
    }

    public bool IsVisibleAt(DateTime utcNow)
    {
        return Status == BannerStatus.Published
            && (StartsAt == null || StartsAt <= utcNow)
            && (EndsAt == null || EndsAt > utcNow);
    }

    private static void Validate(
        string title, string imageUrl, string? linkUrl, DateTime? startsAt, DateTime? endsAt)
    {
        if (string.IsNullOrWhiteSpace(title) || title.Trim().Length > 150)
            throw new ArgumentException("Banner title is required and must not exceed 150 characters.");
        if (!IsHttpUrl(imageUrl) || imageUrl.Trim().Length > 2048)
            throw new ArgumentException("Banner image URL must be a valid HTTP or HTTPS URL.");
        if (!string.IsNullOrWhiteSpace(linkUrl) && !IsHttpUrl(linkUrl))
            throw new ArgumentException("Banner link must be a valid HTTP or HTTPS URL.");
        if (startsAt.HasValue && endsAt.HasValue && endsAt <= startsAt)
            throw new ArgumentException("Banner end date must be later than its start date.");
    }

    private static bool IsHttpUrl(string value)
    {
        return Uri.TryCreate(value, UriKind.Absolute, out var uri)
            && (uri.Scheme == Uri.UriSchemeHttp || uri.Scheme == Uri.UriSchemeHttps);
    }

    private static DateTime? NormalizeUtc(DateTime? value)
    {
        if (!value.HasValue) return null;
        return value.Value.Kind switch
        {
            DateTimeKind.Utc => value.Value,
            DateTimeKind.Local => value.Value.ToUniversalTime(),
            _ => DateTime.SpecifyKind(value.Value, DateTimeKind.Utc)
        };
    }
}
