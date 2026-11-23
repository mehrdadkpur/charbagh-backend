using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.Finance;

public sealed class Payment : BaseEntity
{
    public Guid InvoiceId { get; private set; }

    public Guid PaymentMethodId { get; private set; }

    public Guid? PaidByUserId { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime PaymentDate { get; private set; }

    public string? ReferenceNumber { get; private set; }

    public string? Note { get; private set; }


    // Navigation

    public Invoice Invoice { get; private set; } = null!;

    public PaymentMethod PaymentMethod { get; private set; } = null!;

    public User? PaidByUser { get; private set; }

    public ICollection<Transaction> Transactions { get; private set; }
        = new List<Transaction>();


    // EF Core
    private Payment()
    {
    }


    private Payment(
        Guid invoiceId,
        Guid paymentMethodId,
        Guid? paidByUserId,
        decimal amount,
        DateTime paymentDate,
        string? referenceNumber,
        string? note)
    {
        InvoiceId = invoiceId;
        PaymentMethodId = paymentMethodId;
        PaidByUserId = paidByUserId;
        Amount = amount;
        PaymentDate = paymentDate;
        ReferenceNumber = referenceNumber?.Trim();
        Note = note?.Trim();
    }


    public static Payment Create(
        Guid invoiceId,
        Guid paymentMethodId,
        decimal amount,
        DateTime paymentDate,
        Guid? paidByUserId = null,
        string? referenceNumber = null,
        string? note = null)
    {
        ValidateIds(
            invoiceId,
            paymentMethodId,
            paidByUserId);

        ValidateAmount(amount);

        var payment = new Payment(
            invoiceId,
            paymentMethodId,
            paidByUserId,
            amount,
            paymentDate,
            referenceNumber,
            note);

        payment.MarkAsCreated();

        return payment;
    }


    public void Update(
        Guid paymentMethodId,
        decimal amount,
        DateTime paymentDate,
        Guid? paidByUserId,
        string? referenceNumber,
        string? note)
    {
        ValidateIds(
            null,
            paymentMethodId,
            paidByUserId);

        ValidateAmount(amount);

        PaymentMethodId = paymentMethodId;
        PaidByUserId = paidByUserId;
        Amount = amount;
        PaymentDate = paymentDate;
        ReferenceNumber = referenceNumber?.Trim();
        Note = note?.Trim();

        MarkAsUpdated();
    }


    private static void ValidateIds(
        Guid? invoiceId,
        Guid paymentMethodId,
        Guid? paidByUserId)
    {
        if (invoiceId.HasValue &&
            invoiceId.Value == Guid.Empty)
        {
            throw new ArgumentException(
                "Invoice id is required.",
                nameof(invoiceId));
        }

        if (paymentMethodId == Guid.Empty)
        {
            throw new ArgumentException(
                "Payment method id is required.",
                nameof(paymentMethodId));
        }

        if (paidByUserId.HasValue &&
            paidByUserId.Value == Guid.Empty)
        {
            throw new ArgumentException(
                "Paid by user id is invalid.",
                nameof(paidByUserId));
        }
    }


    private static void ValidateAmount(decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(
                "Payment amount must be greater than zero.",
                nameof(amount));
        }
    }
}