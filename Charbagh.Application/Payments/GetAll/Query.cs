using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.GetAll;

public sealed record GetAllPaymentsQuery: IRequest<IReadOnlyList<PaymentDto>>;