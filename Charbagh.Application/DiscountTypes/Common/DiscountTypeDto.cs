namespace Charbagh.Application.DiscountTypes.Common;

public sealed record DiscountTypeDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    int DisplayOrder);