using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Application.Payments.Common;

public static class PaymentMapper
{
    public static PaymentDto ToDto(
        this Payment payment)
    {
        return new PaymentDto(
            payment.Id,
            payment.InvoiceId,
            payment.PaymentMethodId,
            payment.PaidByUserId,
            payment.Amount,
            payment.PaymentDate,
            payment.ReferenceNumber,
            payment.Note);
    }
}