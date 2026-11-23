using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Discounts.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.Discounts.Create;

public sealed class CreateDiscountHandler : IRequestHandler<CreateDiscountCommand, DiscountDto>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IDiscountTypeRepository _discountTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscountHandler(
        IDiscountRepository discountRepository,
        IInvoiceRepository invoiceRepository,
        IDiscountTypeRepository discountTypeRepository,
        IUnitOfWork unitOfWork)
    {
        _discountRepository = discountRepository;
        _invoiceRepository = invoiceRepository;
        _discountTypeRepository = discountTypeRepository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiscountDto> Handle(
        CreateDiscountCommand request,
        CancellationToken cancellationToken)
    {
        var invoice = await _invoiceRepository
            .GetByIdAsync(
                request.InvoiceId,
                cancellationToken);

        if (invoice is null)
        {
            throw new InvalidOperationException(
                "Invoice was not found.");
        }

        if (invoice.Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Discount can only be applied to draft invoices.");
        }

        if (invoice.Discount is not null)
        {
            throw new InvalidOperationException(
                "Invoice already has a discount.");
        }


        var discountType = await _discountTypeRepository
            .GetByIdAsync(
                request.DiscountTypeId,
                cancellationToken);

        if (discountType is null)
        {
            throw new InvalidOperationException(
                "Discount type was not found.");
        }

        if (!discountType.IsActive)
        {
            throw new InvalidOperationException(
                "Discount type is not active.");
        }


        if (invoice.Subtotal <= 0)
        {
            throw new InvalidOperationException(
                "Invoice subtotal must be greater than zero.");
        }


        var amount = CalculateDiscountAmount(
            invoice.Subtotal,
            request.ValueType,
            request.Value);


        if (amount > invoice.Subtotal)
        {
            throw new InvalidOperationException(
                "Discount amount cannot be greater than invoice subtotal.");
        }


        var discount = Discount.Create(
            request.InvoiceId,
            request.DiscountTypeId,
            request.ValueType,
            request.Value,
            amount,
            request.Description);


        invoice.ApplyDiscount(discount);

        await _discountRepository.AddAsync(
            discount,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);


        return discount.ToDto();
    }


    private static decimal CalculateDiscountAmount(
        decimal subtotal,
        DiscountValueType valueType,
        decimal value)
    {
        if (value <= 0)
        {
            throw new ArgumentException(
                "Discount value must be greater than zero.",
                nameof(value));
        }

        decimal amount;

        switch (valueType)
        {
            case DiscountValueType.FixedAmount:
                amount = value;
                break;

            case DiscountValueType.Percentage:
                if (value > 100)
                {
                    throw new ArgumentException(
                        "Discount percentage cannot be greater than 100.",
                        nameof(value));
                }

                amount = subtotal * value / 100m;
                break;

            default:
                throw new ArgumentOutOfRangeException(
                    nameof(valueType),
                    valueType,
                    "Invalid discount value type.");
        }

        return decimal.Round(
            amount,
            2,
            MidpointRounding.AwayFromZero);
    }
}