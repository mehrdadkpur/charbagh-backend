using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.Update;

public sealed class UpdateDiscountHandler
    : IRequestHandler<UpdateDiscountCommand, DiscountDto>
{
    private readonly IDiscountRepository _discountRepository;
    private readonly IInvoiceRepository _invoiceRepository;
    private readonly IDiscountTypeRepository _discountTypeRepository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiscountHandler(
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

    public async Task<DiscountDto> Handle( UpdateDiscountCommand request, CancellationToken cancellationToken)
    {
        var discount = await _discountRepository
            .GetForUpdateAsync(
                request.Id,
                cancellationToken);

        if (discount is null)
        {
            throw new InvalidOperationException(
                "Discount was not found.");
        }


        var invoice = await _invoiceRepository
            .GetByIdAsync(
                discount.InvoiceId,
                cancellationToken);

        if (invoice is null)
        {
            throw new InvalidOperationException(
                "Invoice was not found.");
        }


        if (invoice.Status != InvoiceStatus.Draft)
        {
            throw new InvalidOperationException(
                "Discount can only be updated on draft invoices.");
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


        var amount = CalculateDiscountAmount(
            invoice.Subtotal,
            request.ValueType,
            request.Value);


        if (amount > invoice.Subtotal)
        {
            throw new InvalidOperationException(
                "Discount amount cannot be greater than invoice subtotal.");
        }


        discount.Update(
            request.DiscountTypeId,
            request.ValueType,
            request.Value,
            amount,
            request.Description);


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