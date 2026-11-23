using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Transactions.Common;
using MediatR;

namespace Charbagh.Application.Transactions.MarkAsSuccessful;

public sealed class MarkTransactionSuccessfulHandler: IRequestHandler< MarkTransactionSuccessfulCommand,TransactionDto>
{
    private readonly ITransactionRepository _transactionRepository;
    private readonly IUnitOfWork _unitOfWork;

    public MarkTransactionSuccessfulHandler(
        ITransactionRepository transactionRepository,
        IUnitOfWork unitOfWork)
    {
        _transactionRepository = transactionRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<TransactionDto> Handle(
        MarkTransactionSuccessfulCommand request,
        CancellationToken cancellationToken)
    {
        var transaction =
            await _transactionRepository.GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (transaction is null)
            throw new KeyNotFoundException(
                "Transaction was not found.");

        transaction.MarkAsSuccessful();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return transaction.ToDto();
    }
}