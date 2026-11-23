
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.GetAll
{
    public sealed class GetAllInvoiceItemsHandler : IRequestHandler<GetAllInvoiceItemsQuery ,IReadOnlyList< InvoiceItemDto>>
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        
        public GetAllInvoiceItemsHandler(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task<IReadOnlyList< InvoiceItemDto>> Handle (GetAllInvoiceItemsQuery request , CancellationToken cancellationToken)
        {
            var invoices = await _invoiceItemRepository.GetAllAsync (cancellationToken);

            return invoices.Select(x => x.ToDto()).ToList();
        }
    }
}
