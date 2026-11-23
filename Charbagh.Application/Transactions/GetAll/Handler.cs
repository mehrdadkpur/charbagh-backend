using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using Charbagh.Application.Transactions.GetAll;
using MediatR;

namespace Charbagh.Application.Features.Finance.Transactions.GetAll;

public sealed class GetAllTransactionsHandler
    : IRequestHandler<
        GetAllTransactionsQuery,
        IReadOnlyList<TransactionDto>>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetAllTransactionsHandler(
        ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<IReadOnlyList<TransactionDto>> Handle(
        GetAllTransactionsQuery request,
        CancellationToken cancellationToken)
    {
        var transactions = await _transactionRepository.GetAllAsync(
            cancellationToken);

        return transactions
            .Select(x => x.ToDto())
            .ToList();
    }
}