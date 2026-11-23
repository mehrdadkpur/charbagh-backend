
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetById
{
    public sealed record GetInvoiceByIdQuery(Guid Id): IRequest<InvoiceDto>;
}
