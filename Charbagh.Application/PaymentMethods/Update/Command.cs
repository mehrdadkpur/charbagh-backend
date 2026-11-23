using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Update;

public sealed record UpdatePaymentMethodCommand(
    Guid Id,
    string Name,
    string? Description,
    int DisplayOrder
) : IRequest<PaymentMethodDto>;