namespace Charbagh.Application.Transactions.Common;

public sealed record TransactionDto(
    Guid Id,
    Guid PaymentId,
    Guid CreatedByUserId,
    TransactionType Type,
    decimal Amount,
    DateTime TransactionDate,
    TransactionStatus Status,
    string? ReferenceNumber,
    string? Description);