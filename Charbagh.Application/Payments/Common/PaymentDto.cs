namespace Charbagh.Application.Payments.Common;

public sealed record PaymentDto(
    Guid Id,
    Guid InvoiceId,
    Guid PaymentMethodId,
    Guid? PaidByUserId,
    decimal Amount,
    DateTime PaymentDate,
    string? ReferenceNumber,
    string? Note);