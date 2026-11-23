using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.MarkAsFailed;

public sealed record MarkTransactionFailedCommand( Guid Id , string? Description = null) : IRequest<TransactionDto>;