
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.GetAll
{
    public sealed record GetAllInvoiceItemsQuery():IRequest<IReadOnlyList< InvoiceItemDto>>;
}
