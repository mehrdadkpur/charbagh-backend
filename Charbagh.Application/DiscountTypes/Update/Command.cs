using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Update;

public sealed record UpdateDiscountTypeCommand( Guid Id, string Name, string? Description,int DisplayOrder) : IRequest<DiscountTypeDto>;