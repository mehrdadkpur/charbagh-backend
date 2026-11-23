using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.Cancel;

public sealed record CancelTransactionCommand( Guid Id , string? Description = null) : IRequest<TransactionDto>;