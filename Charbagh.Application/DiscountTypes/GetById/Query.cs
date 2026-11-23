using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.GetById;

public sealed record GetDiscountTypeByIdQuery( Guid Id) : IRequest<DiscountTypeDto>;