using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Create;

public sealed record CreatePaymentMethodCommand(
    string Name,
    string? Description,
    int DisplayOrder
) : IRequest<PaymentMethodDto>;