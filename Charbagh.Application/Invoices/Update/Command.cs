
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.Update
{
    public sealed record UpdateInvoiceCommand(Guid Id , DateTime InvoiceDate , string Description) : IRequest<InvoiceDto>;
}
