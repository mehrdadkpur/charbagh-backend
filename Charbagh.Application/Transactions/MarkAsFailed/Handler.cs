using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.MarkAsFailed;

public sealed class MarkTransactionFailedHandler: IRequestHandler< MarkTransactionFailedCommand,TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkTransactionFailedHandler(
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto> Handle(
        MarkTransactionFailedCommand request,
        CancellationToken cancellationToken)
    {
        var transaction =
            await _transactionRepository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (transaction is null)
            throw new KeyNotFoundException(
                "Transaction was not found.");

        transaction.MarkAsFailed();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return transaction.ToDto();
    }
}