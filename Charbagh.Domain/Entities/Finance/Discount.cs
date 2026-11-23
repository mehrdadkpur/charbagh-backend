using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Finance;

public sealed class Discount : BaseEntity
{
    public Guid InvoiceId { get; private set; }

    public Guid DiscountTypeId { get; private set; }

    public DiscountValueType ValueType { get; private set; }

    public decimal Value { get; private set; }

    public decimal Amount { get; private set; }

    public string? Description { get; private set; }

    public Invoice Invoice { get; private set; } = null!;

    public DiscountType DiscountType { get; private set; } = null!;

    private Discount()
    {
    }

    private Discount(
        Guid invoiceId,
        Guid discountTypeId,
        DiscountValueType valueType,
        decimal value,
        decimal amount,
        string? description)
    {
        InvoiceId = invoiceId;
        DiscountTypeId = discountTypeId;
        ValueType = valueType;
        Value = value;
        Amount = amount;
        Description = description?.Trim();
    }

    public static Discount Create(
        Guid invoiceId,
        Guid discountTypeId,
        DiscountValueType valueType,
        decimal value,
        decimal amount,
        string? description = null)
    {
        ValidateIds(invoiceId, discountTypeId);
        ValidateValue(valueType, value);
        ValidateAmount(amount);

        var discount = new Discount(
            invoiceId,
            discountTypeId,
            valueType,
            value,
            amount,
            description);

        discount.MarkAsCreated();

        return discount;
    }

    public void Update(
        Guid discountTypeId,
        DiscountValueType valueType,
        decimal value,
        decimal amount,
        string? description)
    {
        ValidateIds(null, discountTypeId);
        ValidateValue(valueType, value);
        ValidateAmount(amount);

        DiscountTypeId = discountTypeId;
        ValueType = valueType;
        Value = value;
        Amount = amount;
        Description = description?.Trim();

        MarkAsUpdated();
    }


    private static void ValidateIds(
        Guid? invoiceId,
        Guid discountTypeId)
    {
        if (invoiceId.HasValue && invoiceId.Value == Guid.Empty)
        {
            throw new ArgumentException(
                "Invoice id is required.",
                nameof(invoiceId));
        }

        if (discountTypeId == Guid.Empty)
        {
            throw new ArgumentException(
                "Discount type id is required.",
                nameof(discountTypeId));
        }
    }

    private static void ValidateValue(
        DiscountValueType valueType,
        decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                "Discount value must be greater than zero.",
                nameof(value));
        }

        if (valueType == DiscountValueType.Percentage &&
            value > 100)
        {
            throw new ArgumentException(
                "Discount percentage cannot be greater than 100.",
                nameof(value));
        }
    }

    private static void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(
                "Discount amount must be greater than zero.",
                nameof(amount));
        }
    }
}