using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.GetByPayment;

public sealed record GetTransactionsByPaymentQuery( Guid PaymentId) : IRequest<IReadOnlyList<TransactionDto>>;