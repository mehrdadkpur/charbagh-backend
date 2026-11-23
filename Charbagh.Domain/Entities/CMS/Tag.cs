namespace Charbagh.Domain.Entities.CMS;

public sealed class Tag : BaseEntity
{
    public string Name { get; private set; } = null!;

    public bool IsActive { get; private set; }

    public ICollection<Blog> Blogs { get; private set; } = new List<Blog>();

    private Tag()
    {
    }

    private Tag(string name)
    {
        Name = name;
        IsActive = true;
    }

    public static Tag Create(string name)
    {
        Validate(name);

        var tag = new Tag(name.Trim());

        tag.MarkAsCreated();

        return tag;
    }

    public void Update(string name)
    {
        Validate(name);

        Name = name.Trim();

        MarkAsUpdated();
    }

    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        MarkAsUpdated();
    }

    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;

        MarkAsUpdated();
    }

    private static void Validate(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Tag name is required.",
                nameof(name));

        if (name.Trim().Length < 2)
            throw new ArgumentException(
                "Tag name must be at least 2 characters.",
                nameof(name));

        if (name.Trim().Length > 50)
            throw new ArgumentException(
                "Tag name cannot be longer than 50 characters.",
                nameof(name));
    }
}