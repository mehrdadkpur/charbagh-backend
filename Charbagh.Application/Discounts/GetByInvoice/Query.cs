using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.GetByInvoice;

public sealed record GetDiscountByInvoiceQuery( Guid InvoiceId) : IRequest<DiscountDto?>;