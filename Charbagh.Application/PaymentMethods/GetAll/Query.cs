using Charbagh.Application.PaymentMethods.Common;
using MediatR;

namespace Charbagh.Application.PaymentMethods.GetAll;

public sealed record GetAllPaymentMethodsQuery : IRequest<IReadOnlyList<PaymentMethodDto>>;