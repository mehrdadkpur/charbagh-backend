using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.Create;

public sealed record CreateTransactionCommand(
    Guid PaymentId,
    Guid CreatedByUserId,
    TransactionType Type,
    decimal Amount,
    DateTime TransactionDate,
    string? ReferenceNumber,
    string? Description) : IRequest<TransactionDto>;