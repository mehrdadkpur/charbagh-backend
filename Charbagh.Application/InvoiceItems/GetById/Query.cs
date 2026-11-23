
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.GetById
{
    public sealed record GetInvoiceItemByIdQuery(Guid Id) : IRequest<InvoiceItemDto>;
}
