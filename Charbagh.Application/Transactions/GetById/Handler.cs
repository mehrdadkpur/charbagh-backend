using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using Charbagh.Application.Transactions.GetById;
using MediatR;

namespace Charbagh.Application.Features.Finance.Transactions.GetById;

public sealed class GetTransactionByIdHandler : IRequestHandler<GetTransactionByIdQuery, TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;

    public GetTransactionByIdHandler(ITransactionRepository transactionRepository)
    {
        _transactionRepository = transactionRepository;
    }

    public async Task<TransactionDto> Handle( GetTransactionByIdQuery request, CancellationToken cancellationToken)
    {
        var transaction = await _transactionRepository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (transaction is null)
            throw new KeyNotFoundException(
                "Transaction was not found.");

        return transaction.ToDto();
    }
}