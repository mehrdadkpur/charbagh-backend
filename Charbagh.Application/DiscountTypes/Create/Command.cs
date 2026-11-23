using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Create;

public sealed record CreateDiscountTypeCommand(
    string Name,
    string? Description,
    int DisplayOrder) : IRequest<DiscountTypeDto>;