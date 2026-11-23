
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InvoiceItems.Common;
using MediatR;

namespace Charbagh.Application.InvoiceItems.Update
{
    public sealed class UpdateInvoiceItemHandler
        : IRequestHandler<UpdateInvoiceItemCommand, InvoiceItemDto>
    {
        private readonly IInvoiceItemRepository _invoiceItemRepository;
        private readonly IServicePriceRepository _priceRepository;
        private readonly IInvoiceRepository _invoiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateInvoiceItemHandler(
            IInvoiceItemRepository invoiceItemRepository,
            IServicePriceRepository servicePriceRepository,
            IInvoiceRepository invoiceRepository,
            IUnitOfWork unitOfWork)
        {
            _invoiceItemRepository = invoiceItemRepository;
            _priceRepository = servicePriceRepository;
            _invoiceRepository = invoiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InvoiceItemDto> Handle(
            UpdateInvoiceItemCommand request,
            CancellationToken cancellationToken)
        {
            var invoiceItem = await _invoiceItemRepository.GetByIdAsync(
                request.Id,
                cancellationToken);

            if (invoiceItem is null)
            {
                throw new InvoiceItemNotFoundException(request.Id);
            }


            var invoice = await _invoiceRepository.GetByIdAsync(
                request.InvoiceId,
                cancellationToken);

            if (invoice is null)
            {
                throw new InvoiceNotFoundException(request.InvoiceId);
            }


            if (invoice.Status != InvoiceStatus.Draft)
            {
                throw new InvalidOperationException(
                    "Invoice items can only be updated while the invoice is in draft status.");
            }

            if (invoiceItem.InvoiceId != request.InvoiceId)
            {
                throw new InvalidOperationException(
                    "Invoice item does not belong to the specified invoice.");
            }


            decimal unitPrice;

            if (request.Type == InvoiceItemType.Other)
            {
                unitPrice = request.UnitPrice;
            }
            else
            {
                if (request.ServicePriceId is null)
                {
                    throw new ArgumentException(
                        "Service price is required for this invoice item type.",
                        nameof(request.ServicePriceId));
                }

                var servicePrice = await _priceRepository.GetByIdAsync(
                    request.ServicePriceId.Value,
                    cancellationToken);

                if (servicePrice is null)
                {
                    throw new ServicePriceNotFoundException(
                        request.ServicePriceId.Value);
                }

                unitPrice = servicePrice.Price;
            }


            invoiceItem.Update(
                request.ServicePriceId,
                request.Type,
                request.Description,
                unitPrice,
                request.Quantity);


            await _unitOfWork.SaveChangesAsync(
                cancellationToken);

            return invoiceItem.ToDto();
        }
    }
}
