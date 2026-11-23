using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.Create;

public sealed record CreateDiscountCommand(
    Guid InvoiceId,
    Guid DiscountTypeId,
    DiscountValueType ValueType,
    decimal Value,
    string? Description) : IRequest<DiscountDto>;