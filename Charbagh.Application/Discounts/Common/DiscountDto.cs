namespace Charbagh.Application.Discounts.Common;

public sealed record DiscountDto(
    Guid Id,
    Guid InvoiceId,
    Guid DiscountTypeId,
    DiscountValueType ValueType,
    decimal Value,
    decimal Amount,
    string? Description);