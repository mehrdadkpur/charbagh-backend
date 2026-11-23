
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Invoices.Common;
using MediatR;

namespace Charbagh.Application.Invoices.CancelInvoice
{
    public sealed class CancelInvoiceHandler : IRequestHandler<CancelInvoiceCommand , InvoiceDto>
    {
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CancelInvoiceHandler(IInvoiceRepository invoiceRepository, IUnitOfWork unitOfWork)
        {
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <InvoiceDto> Handle(CancelInvoiceCommand request , CancellationToken cancellationToken)
        {
            var invoice = await _invoiceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (invoice == null)
            {
                throw new InvoiceNotFoundException(request.Id);
            }

            invoice.Cancel();
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return invoice.ToDto();
        }
    }
}
