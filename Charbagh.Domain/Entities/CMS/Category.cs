using Charbagh.Domain.Entities.CMS;

public sealed class Category : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }


    // Navigation Properties

    public ICollection<Blog> Blogs { get; private set; } = new List<Blog>();


    // Constructor

    private Category()
    {
    }


    private Category(
        string name,
        string? description)
    {
        Name = name;
        Description = description;
        IsActive = true;
    }

    public static Category Create(
        string name, 
        string? description = null
        ) 
    { 
        Validate(name); 
        
        var category = new Category(name.Trim(), description?.Trim());
        
        category.MarkAsCreated(); 
        return category; 
    } 
    // Update

    public void Update( string name, string? description) 
    { 
        Validate(name); 
        Name = name.Trim(); 
        Description = description?.Trim(); 
        MarkAsUpdated(); 
    } 

    // Status
    public void Activate() 
    { 
        if (IsActive) 
        { 
            return; 
        } 
        IsActive = true; 
        MarkAsUpdated(); 
    } 
    public void Deactivate() 
    { 
        if (!IsActive) 
        { 
            return; 
        } 
        IsActive = false;
        MarkAsUpdated(); 
    } 

    // Validation
    
    private static void Validate(string name) 
    { 
        if (string.IsNullOrWhiteSpace(name)) 
        { 
            throw new ArgumentException( "Category name is required.", nameof(name)); 
        } 

        if (name.Trim().Length > 100) 
        { 
            throw new ArgumentException( "Category name cannot be longer than 100 characters.", nameof(name));
        } 

        if (name.Trim().Length < 2) 
        {
            throw new ArgumentException( "Category name must be at least 2 characters.", nameof(name)); 
        } 
    }
}