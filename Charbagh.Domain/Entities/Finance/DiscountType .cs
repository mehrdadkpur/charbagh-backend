using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Finance;

public sealed class DiscountType : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public int DisplayOrder { get; private set; }


    // Navigation

    public ICollection<Discount> Discounts { get; private set; }
        = new List<Discount>();


    // Constructors

    private DiscountType()
    {
        // Required by EF Core
    }

    private DiscountType(
        string name,
        string? description,
        int displayOrder)
    {
        Name = name.Trim();
        Description = description?.Trim();
        DisplayOrder = displayOrder;
        IsActive = true;
    }


    // Factory

    public static DiscountType Create(
        string name,
        string? description = null,
        int displayOrder = 0)
    {
        ValidateName(name);
        ValidateDisplayOrder(displayOrder);

        var discountType = new DiscountType(
            name,
            description,
            displayOrder);

        discountType.MarkAsCreated();

        return discountType;
    }


    // Update

    public void Update(
        string name,
        string? description,
        int displayOrder)
    {
        ValidateName(name);
        ValidateDisplayOrder(displayOrder);

        Name = name.Trim();
        Description = description?.Trim();
        DisplayOrder = displayOrder;

        MarkAsUpdated();
    }


    // Status

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


    // Validation

    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
        {
            throw new ArgumentException(
                "Discount type name is required.",
                nameof(name));
        }
    }

    private static void ValidateDisplayOrder(int displayOrder)
    {
        if (displayOrder < 0)
        {
            throw new ArgumentException(
                "Display order cannot be negative.",
                nameof(displayOrder));
        }
    }
}