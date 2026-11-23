using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.Update;

public sealed record UpdatePaymentCommand(
    Guid Id,
    Guid PaymentMethodId,
    Guid? PaidByUserId,
    decimal Amount,
    DateTime PaymentDate,
    string? ReferenceNumber,
    string? Note) : IRequest<PaymentDto>;