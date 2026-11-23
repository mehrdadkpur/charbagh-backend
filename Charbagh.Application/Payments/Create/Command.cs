using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.Create;

public sealed record CreatePaymentCommand(
    Guid InvoiceId,
    Guid PaymentMethodId,
    Guid? PaidByUserId,
    decimal Amount,
    DateTime PaymentDate,
    string? ReferenceNumber,
    string? Note) : IRequest<PaymentDto>;