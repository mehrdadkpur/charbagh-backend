
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetAll
{
    public sealed class GetAllInvoicesHandler : IRequestHandler<GetAllInvoicesQuery,IReadOnlyList< InvoiceDto>>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetAllInvoicesHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task<IReadOnlyList< InvoiceDto>> Handle(GetAllInvoicesQuery request, CancellationToken cancellationToken)
        {
            var invoices = await _invoiceRepository.GetAllAsync(cancellationToken);

            return invoices.Select(x => x.ToDto()).ToList();
        }
    }
}
