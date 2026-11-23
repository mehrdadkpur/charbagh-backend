using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.MarkAsSuccessful;

public sealed record MarkTransactionSuccessfulCommand( Guid Id) : IRequest<TransactionDto>;