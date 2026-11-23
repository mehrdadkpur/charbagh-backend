using Charbagh.Application.Transactions;
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Transactions.Common;

public static class TransactionMapper
{
    public static TransactionDto ToDto(
        this Transaction transaction)
    {
        return new TransactionDto(
            transaction.Id,
            transaction.PaymentId,
            transaction.CreatedByUserId,
            transaction.Type,
            transaction.Amount,
            transaction.TransactionDate,
            transaction.Status,
            transaction.ReferenceNumber,
            transaction.Description);
    }
}