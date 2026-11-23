
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.ConfirmInvoice
{
    public sealed record ConfirmInvoiceCommand(Guid Id) : IRequest<InvoiceDto>;
}
