using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.GetById;

public sealed record GetTransactionByIdQuery(Guid Id) : IRequest<TransactionDto>;