using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.GetByPayment;

public sealed class GetTransactionsByPaymentHandler
    : IRequestHandler<
        GetTransactionsByPaymentQuery,
        IReadOnlyList<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionsByPaymentHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IReadOnlyList<TransactionDto>> Handle(
        GetTransactionsByPaymentQuery request,
        CancellationToken cancellationToken)
    {
        var transactions =
            await _transactionRepository.GetByPaymentIdAsync(
                request.PaymentId,
                cancellationToken);

        return transactions
            .Select(x => x.ToDto())
            .ToList();
    }
}