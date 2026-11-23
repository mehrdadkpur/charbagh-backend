using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.GetById;

public sealed record GetPaymentByIdQuery( Guid Id) : IRequest<PaymentDto>;