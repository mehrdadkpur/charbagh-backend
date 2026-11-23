using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetByStudentId
{
    public sealed record GetAllInvoicesByStudentIdQuery(Guid StudentId) : IRequest<IReadOnlyList< InvoiceDto>>;
}
