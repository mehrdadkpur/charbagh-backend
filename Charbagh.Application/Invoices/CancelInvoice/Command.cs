
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.CancelInvoice
{
    public sealed record CancelInvoiceCommand(Guid Id) : IRequest<InvoiceDto>;
}
