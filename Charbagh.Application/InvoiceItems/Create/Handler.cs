using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InvoiceItems.Common;
using Charbagh.Application.InvoiceItems.Create;
using Charbagh.Domain.Entities.Finance;
using MediatR;

public sealed class CreateInvoiceItemHandler: IRequestHandler<CreateInvoiceItemCommand, InvoiceItemDto>
{
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IInvoiceItemRepository _invoiceItemRepository;
    private readonly IServicePriceRepository _servicePriceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInvoiceItemHandler(
        IInvoiceRepository invoiceRepository,
        IInvoiceItemRepository invoiceItemRepository,
        IServicePriceRepository servicePriceRepository,
        IUnitOfWork unitOfWork)
    {
        _invoiceRepository = invoiceRepository;
        _invoiceItemRepository = invoiceItemRepository;
        _servicePriceRepository = servicePriceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InvoiceItemDto> Handle(
        CreateInvoiceItemCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository.GetByIdAsync(
            request.InvoiceId,
            cancellationToken);

        if (invoice is null)
        {
            throw new InvoiceNotFoundException(
                request.InvoiceId);
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
                throw new ArgumentException("Service price is required for this invoice item type.", nameof(request.ServicePriceId));
            }

            var servicePrice = await _servicePriceRepository.GetByIdAsync(
                request.ServicePriceId.Value,
                cancellationToken);

            if (servicePrice is null)
            {
                throw new ServicePriceNotFoundException(
                    request.ServicePriceId.Value);
            }

            unitPrice = servicePrice.Price;
        }

        var invoiceItem = InvoiceItem.Create(
            request.InvoiceId,
            request.ServicePriceId,
            request.Type,
            request.Description,
            unitPrice,
            request.Quantity);

        invoice.AddItem(invoiceItem);

        await _invoiceItemRepository.AddAsync(
            invoiceItem,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return invoiceItem.ToDto();
    }
}