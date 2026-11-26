using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.CMS;

public sealed class Blog : BaseEntity
{
    public Guid UserId { get; private set; }

    public Guid CategoryId { get; private set; }

    public string Title { get; private set; } = null!;

    public string Text { get; private set; } = null!;

    public string? ImageUrl { get; private set; }

    public BlogStatus Status { get; private set; }


    // Navigation Properties

    public User Author { get; private set; } = null!;

    public Category Category { get; private set; } = null!;

    public ICollection<Comment>  Comments { get; private set; } = new List<Comment>();

    public ICollection<Tag> Tags { get; private set; } = new List<Tag>();


    // Constructor

    private Blog()
    {
    }


    private Blog(
        Guid userId,
        Guid categoryId,
        string title,
        string text,
        string? imageUrl)
    {
        UserId = userId;
        CategoryId = categoryId;
        Title = title;
        Text = text;
        ImageUrl = imageUrl;
        Status = BlogStatus.Draft;
    }


    // Factory

    public static Blog Create(
        Guid userId,
        Guid categoryId,
        string title,
        string text,
        string? imageUrl = null)
    {
        if (userId == Guid.Empty)
        {
            throw new ArgumentException(
                "Author is required.",
                nameof(userId));
        }

        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException(
                "Blog category is required.",
                nameof(categoryId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Blog title is required.",
                nameof(title));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(
                "Blog text is required.",
                nameof(text));
        }

        var blog = new Blog(
            userId,
            categoryId,
            title.Trim(),
            text.Trim(),
            imageUrl?.Trim());

        blog.MarkAsCreated();

        return blog;
    }


    // Update

    public void Update(
        Guid categoryId,
        string title,
        string text,
        string? imageUrl)
    {
        if (categoryId == Guid.Empty)
        {
            throw new ArgumentException(
                "Blog category is required.",
                nameof(categoryId));
        }

        if (string.IsNullOrWhiteSpace(title))
        {
            throw new ArgumentException(
                "Blog title is required.",
                nameof(title));
        }

        if (string.IsNullOrWhiteSpace(text))
        {
            throw new ArgumentException(
                "Blog text is required.",
                nameof(text));
        }

        if (Status == BlogStatus.Published)
        {
            throw new InvalidOperationException(
                "Published blogs cannot be updated.");
        }

        if (Status == BlogStatus.Archived)
        {
            throw new InvalidOperationException(
                "Archived blogs cannot be updated.");
        }

        CategoryId = categoryId;
        Title = title.Trim();
        Text = text.Trim();
        ImageUrl = imageUrl?.Trim();

        MarkAsUpdated();
    }


    // Status

    public void Publish()
    {
        if (Status == BlogStatus.Published)
        {
            return;
        }

        if (Status == BlogStatus.Archived)
        {
            throw new InvalidOperationException(
                "Archived blogs cannot be published.");
        }

        Status = BlogStatus.Published;

        MarkAsUpdated();
    }


    public void Unpublish()
    {
        if (Status != BlogStatus.Published)
        {
            return;
        }

        Status = BlogStatus.Draft;

        MarkAsUpdated();
    }


    public void Archive()
    {
        if (Status == BlogStatus.Archived)
        {
            return;
        }

        Status = BlogStatus.Archived;

        MarkAsUpdated();
    }


    public void Restore()
    {
        if (Status != BlogStatus.Archived)
        {
            return;
        }

        Status = BlogStatus.Draft;

        MarkAsUpdated();
    }


    // Computed State

    public bool IsDraft =>
        Status == BlogStatus.Draft;

    public bool IsPublished =>
        Status == BlogStatus.Published;

    public bool IsArchived =>
        Status == BlogStatus.Archived;
}