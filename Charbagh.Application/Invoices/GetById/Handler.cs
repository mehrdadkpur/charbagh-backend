
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.GetById
{
    public sealed class GetInvoiceByIdHandler : IRequestHandler<GetInvoiceByIdQuery , InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;

        public GetInvoiceByIdHandler(IInvoiceRepository invoiceRepository)
        {
            _invoiceRepository = invoiceRepository;
        }

        public async Task <InvoiceDto> Handle (GetInvoiceByIdQuery request , CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException(request.Id);
            }

            return invoice.ToDto();
        }
    }
}
