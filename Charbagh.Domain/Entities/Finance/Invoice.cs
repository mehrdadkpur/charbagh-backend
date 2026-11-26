using Charbagh.Domain.Common;
using Charbagh.Domain.Entities.People;

namespace Charbagh.Domain.Entities.Finance;

public sealed class Invoice : BaseEntity
{
    public Guid StudentId { get; private set; }

    public DateTime InvoiceDate { get; private set; }

    public decimal PaidAmount { get; private set; }

    public InvoiceStatus Status { get; private set; }

    public string? Description { get; private set; }


    // Computed Amounts

    public decimal Subtotal =>
        Items.Sum(x => x.TotalAmount);

    public decimal DiscountAmount =>
        Discount?.Amount ?? 0;

    public decimal TotalAmount =>
        Subtotal - DiscountAmount;

    public decimal RemainingAmount =>
        TotalAmount - PaidAmount;


    // Computed State

    public bool IsPaid =>
        Status == InvoiceStatus.Paid;

    public bool IsCancelled =>
        Status == InvoiceStatus.Cancelled;

    public bool HasRemainingAmount =>
        RemainingAmount > 0.01m;


    // Navigation Properties

    public Student Student { get; private set; } = null!;

    public Discount? Discount { get; private set; }

    public ICollection<InvoiceItem> Items { get; private set; }
        = new List<InvoiceItem>();

    public ICollection<Payment> Payments { get; private set; }
        = new List<Payment>();


    // Constructors

    private Invoice()
    {
    }

    private Invoice(
        Guid studentId,
        DateTime invoiceDate,
        string? description)
    {
        StudentId = studentId;
        InvoiceDate = invoiceDate;
        PaidAmount = 0;
        Status = InvoiceStatus.Draft;
        Description = description?.Trim();
    }


    // Factory

    public static Invoice Create(
        Guid studentId,
        DateTime invoiceDate,
        string? description = null)
    {
        if (studentId == Guid.Empty)
        {
            throw new ArgumentException(
                "Student is required.",
                nameof(studentId));
        }

        var invoice = new Invoice(
            studentId,
            invoiceDate,
            description);

        invoice.MarkAsCreated();

        return invoice;
    }


    // Update

    public void Update(
        DateTime invoiceDate,
        string? description)
    {
        if (Status == InvoiceStatus.Paid)
        {
            throw new InvalidOperationException(
                "Paid invoices cannot be updated.");
        }

        if (Status == InvoiceStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Cancelled invoices cannot be updated.");
        }

        InvoiceDate = invoiceDate;
        Description = description?.Trim();

        MarkAsUpdated();
    }


    // Invoice Items

    public void AddItem(InvoiceItem item)
    {
        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Items can only be added to draft invoices.");
        }

        if (Discount is not null)
        {
            throw new InvalidOperationException(
                "Invoice items cannot be changed after applying a discount.");
        }

        if (item is null)
        {
            throw new ArgumentNullException(nameof(item));
        }

        Items.Add(item);

        MarkAsUpdated();
    }


    public void RemoveItem(Guid invoiceItemId)
    {
        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Items can only be removed from draft invoices.");
        }

        if (Discount is not null)
        {
            throw new InvalidOperationException(
                "Invoice items cannot be changed after applying a discount.");
        }

        var item = Items.FirstOrDefault(
            x => x.Id == invoiceItemId);

        if (item is null)
        {
            throw new InvalidOperationException(
                "Invoice item was not found.");
        }

        Items.Remove(item);

        MarkAsUpdated();
    }


    // Discount

    public void ApplyDiscount(Discount discount)
    {
        if (discount is null)
        {
            throw new ArgumentNullException(nameof(discount));
        }

        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Discount can only be applied to draft invoices.");
        }

        if (Discount is not null)
        {
            throw new InvalidOperationException(
                "Invoice already has a discount.");
        }

        if (discount.InvoiceId != Id)
        {
            throw new InvalidOperationException(
                "Discount does not belong to this invoice.");
        }

        if (discount.Amount <= 0)
        {
            throw new InvalidOperationException(
                "Discount amount must be greater than zero.");
        }

        if (discount.Amount > Subtotal)
        {
            throw new InvalidOperationException(
                "Discount amount cannot be greater than invoice subtotal.");
        }

        Discount = discount;

        MarkAsUpdated();
    }


    public void RemoveDiscount()
    {
        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Discount can only be removed from draft invoices.");
        }

        if (Discount is null)
        {
            return;
        }

        Discount = null;

        MarkAsUpdated();
    }


    // Payment

    public void AddPayment(decimal amount)
    {
        if (Status == InvoiceStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Cancelled invoices cannot receive payments.");
        }

        if (Status == InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Draft invoices cannot receive payments.");
        }

        if (Status == InvoiceStatus.Paid)
        {
            throw new InvalidOperationException(
                "Paid invoices cannot receive payments.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException(
                "Payment amount must be greater than zero.",
                nameof(amount));
        }

        if (amount > RemainingAmount)
        {
            throw new InvalidOperationException(
                "Payment amount cannot be greater than remaining invoice amount.");
        }

        PaidAmount += amount;

        UpdatePaymentStatus();

        MarkAsUpdated();
    }


    public void RefundPayment(decimal amount)
    {
        if (Status == InvoiceStatus.Draft ||
            Status == InvoiceStatus.Cancelled)
        {
            throw new InvalidOperationException(
                "Draft or cancelled invoices cannot be refunded.");
        }

        if (amount <= 0)
        {
            throw new ArgumentException(
                "Refund amount must be greater than zero.",
                nameof(amount));
        }

        if (amount > PaidAmount)
        {
            throw new InvalidOperationException(
                "Refund amount cannot be greater than the paid amount.");
        }

        PaidAmount -= amount;
        UpdatePaymentStatus();
        MarkAsUpdated();
    }


    // Payment Status

    private void UpdatePaymentStatus()
    {
        if (PaidAmount <= 0)
        {
            PaidAmount = 0;
            Status = InvoiceStatus.Unpaid;
            return;
        }

        if (PaidAmount < TotalAmount)
        {
            Status = InvoiceStatus.PartiallyPaid;
            return;
        }

        PaidAmount = TotalAmount;
        Status = InvoiceStatus.Paid;
    }


    // Invoice Status

    public void Confirm()
    {
        if (Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Only draft invoices can be confirmed.");
        }

        if (!Items.Any())
        {
            throw new InvalidOperationException(
                "Invoice must have at least one item.");
        }

        if (Subtotal <= 0)
        {
            throw new InvalidOperationException(
                "Invoice subtotal must be greater than zero.");
        }

        if (TotalAmount <= 0)
        {
            throw new InvalidOperationException(
                "Invoice total amount must be greater than zero.");
        }

        Status = InvoiceStatus.Unpaid;

        MarkAsUpdated();
    }


    public void Cancel()
    {
        if (Status == InvoiceStatus.Paid)
        {
            throw new InvalidOperationException(
                "Paid invoices cannot be cancelled.");
        }

        if (PaidAmount > 0)
        {
            throw new InvalidOperationException(
                "Invoices with received payments must be fully refunded before cancellation.");
        }

        if (Status == InvoiceStatus.Cancelled)
        {
            return;
        }

        Status = InvoiceStatus.Cancelled;

        MarkAsUpdated();
    }
}