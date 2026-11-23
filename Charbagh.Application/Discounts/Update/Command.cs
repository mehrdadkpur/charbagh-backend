using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.Update;

public sealed record UpdateDiscountCommand(
    Guid Id,
    Guid DiscountTypeId,
    DiscountValueType ValueType,
    decimal Value,
    string? Description) : IRequest<DiscountDto>;