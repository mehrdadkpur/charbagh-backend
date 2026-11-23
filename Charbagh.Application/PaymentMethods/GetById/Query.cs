using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.GetById;

public sealed record GetPaymentMethodByIdQuery(Guid Id) : IRequest<PaymentMethodDto>;