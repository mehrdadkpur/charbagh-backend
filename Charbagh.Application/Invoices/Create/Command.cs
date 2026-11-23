
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.Create
{
    public sealed record CreateInvoiceCommand( Guid StudentId, string? Description ) :IRequest<InvoiceDto>;
}
