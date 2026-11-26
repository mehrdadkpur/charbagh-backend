using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.Finance;

public sealed class Transaction : BaseEntity
{
    public Guid PaymentId { get; private set; }

    public Guid CreatedByUserId { get; private set; }

    public TransactionType Type { get; private set; }

    public decimal Amount { get; private set; }

    public DateTime TransactionDate { get; private set; }

    public TransactionStatus Status { get; private set; }

    public string? ReferenceNumber { get; private set; }

    public string? Description { get; private set; }

    public Payment Payment { get; private set; } = null!;

    public User CreatedByUser { get; private set; } = null!;


    private Transaction()
    {
    }

    private Transaction(
        Guid paymentId,
        Guid createdByUserId,
        TransactionType type,
        decimal amount,
        DateTime transactionDate,
        string? referenceNumber,
        string? description)
    {
        PaymentId = paymentId;
        CreatedByUserId = createdByUserId;
        Type = type;
        Amount = amount;
        TransactionDate = transactionDate;
        Status = TransactionStatus.Pending;
        ReferenceNumber = referenceNumber?.Trim();
        Description = description?.Trim();
    }


    public static Transaction Create(
        Guid paymentId,
        Guid createdByUserId,
        TransactionType type,
        decimal amount,
        DateTime transactionDate,
        string? referenceNumber = null,
        string? description = null)
    {
        ValidateIds(paymentId, createdByUserId);
        ValidateAmount(amount);

        if (!Enum.IsDefined(typeof(TransactionType), type))
        {
            throw new ArgumentOutOfRangeException(
                nameof(type),
                "Transaction type is invalid.");
        }

        var transaction = new Transaction(
            paymentId,
            createdByUserId,
            type,
            amount,
            transactionDate,
            referenceNumber,
            description);

        transaction.MarkAsCreated();

        return transaction;
    }


    // State Management

    public void MarkAsSuccessful()
    {
        if (Status == TransactionStatus.Successful)
            return;

        EnsurePending();

        Status = TransactionStatus.Successful;
        MarkAsUpdated();
    }

    public void MarkAsFailed(string? description = null)
    {
        EnsurePending();

        Status = TransactionStatus.Failed;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description.Trim();

        MarkAsUpdated();
    }

    public void Cancel(string? description = null)
    {
        EnsurePending();

        Status = TransactionStatus.Cancelled;

        if (!string.IsNullOrWhiteSpace(description))
            Description = description.Trim();

        MarkAsUpdated();
    }

    private void EnsurePending()
    {
        if (Status != TransactionStatus.Pending)
        {
            throw new InvalidOperationException(
                "Only pending transactions can change their status.");
        }
    }


    public void UpdateReferenceNumber(
        string? referenceNumber)
    {
        ReferenceNumber = referenceNumber?.Trim();

        MarkAsUpdated();
    }

    public void UpdateDescription(
        string? description)
    {
        Description = description?.Trim();

        MarkAsUpdated();
    }


    // Validation

    private static void ValidateIds(
        Guid paymentId,
        Guid createdByUserId)
    {
        if (paymentId == Guid.Empty)
        {
            throw new ArgumentException(
                "Payment id is required.",
                nameof(paymentId));
        }

        if (createdByUserId == Guid.Empty)
        {
            throw new ArgumentException(
                "Created by user id is required.",
                nameof(createdByUserId));
        }
    }

    private static void ValidateAmount(
        decimal amount)
    {
        if (amount <= 0)
        {
            throw new ArgumentException(
                "Transaction amount must be greater than zero.",
                nameof(amount));
        }
    }
}