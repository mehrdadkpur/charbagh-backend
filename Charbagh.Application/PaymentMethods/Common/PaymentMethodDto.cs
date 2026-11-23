namespace Charbagh.Application.PaymentMethods.Common;

public sealed record PaymentMethodDto(
    Guid Id,
    string Name,
    string? Description,
    bool IsActive,
    int DisplayOrder);