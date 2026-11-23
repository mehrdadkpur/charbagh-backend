
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.GetById
{
    public sealed class GetInvoiceItemByIdHandler : IRequestHandler<GetInvoiceItemByIdQuery, InvoiceItemDto>
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;

        public GetInvoiceItemByIdHandler(IInvoiceItemRepository invoiceItemRepository)
        {
            _invoiceItemRepository = invoiceItemRepository;
        }

        public async Task<InvoiceItemDto> Handle(GetInvoiceItemByIdQuery request, CancellationToken cancellationToken)
        {
            var invoice = await _invoiceItemRepository.GetByIdAsync(request.Id , cancellationToken);

            if(invoice == null)
            {
                throw new InvoiceItemNotFoundException(request.Id);
            }

            return invoice.ToDto();
        }
    }
}
