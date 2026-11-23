using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.Activate;

public sealed record ActivatePaymentMethodCommand(Guid Id) : IRequest<PaymentMethodDto>;