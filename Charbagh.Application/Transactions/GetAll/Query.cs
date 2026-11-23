using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.GetAll;

public sealed record GetAllTransactionsQuery: IRequest<IReadOnlyList<TransactionDto>>;