using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Activate;

public sealed record ActivateDiscountTypeCommand( Guid Id) : IRequest<DiscountTypeDto>;