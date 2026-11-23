using Charbagh.Application.Payments.Common;
using MediatR;

namespace Charbagh.Application.Payments.GetByInvoiceId;

public sealed record GetPaymentsByInvoiceQuery( Guid InvoiceId) : IRequest<IReadOnlyList<PaymentDto>>;