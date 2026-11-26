using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.InvoiceItems.Remove;

public sealed class RemoveInvoiceItemHandler : IRequestHandler<RemoveInvoiceItemCommand>
{
    private readonly IInvoiceItemRepository _itemRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IUnitOfWork _unitOfWork;

    public RemoveInvoiceItemHandler(
        IInvoiceItemRepository itemRepository,
        IInvoiceRepository invoiceRepository,
        IUnitOfWork unitOfWork)
    {
        _itemRepository = itemRepository;
        _invoiceRepository = invoiceRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task Handle(RemoveInvoiceItemCommand request, CancellationToken cancellationToken)
    {
        var item = await _itemRepository.GetByIdAsync(request.Id, cancellationToken);
        if (item is null)
            throw new InvoiceItemNotFoundException(request.Id);

        var invoice = await _invoiceRepository.GetForUpdateAsync(item.InvoiceId, cancellationToken);
        if (invoice is null)
            throw new InvoiceNotFoundException(item.InvoiceId);

        invoice.RemoveItem(item.Id);
        await _unitOfWork.SaveChangesAsync(cancellationToken);
    }
}
