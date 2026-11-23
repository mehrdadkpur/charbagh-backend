using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Deactivate;

public sealed record DeactivatePaymentMethodCommand(Guid Id) : IRequest<PaymentMethodDto>;