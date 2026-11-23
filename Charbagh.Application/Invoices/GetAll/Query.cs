using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetAll
{
    public sealed record GetAllInvoicesQuery(): IRequest<IReadOnlyList< InvoiceDto>>;
}
