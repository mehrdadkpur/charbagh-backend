using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Deactivate;

public sealed record DeactivateDiscountTypeCommand( Guid Id) : IRequest<DiscountTypeDto>;