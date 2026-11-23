using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.GetById;

public sealed record GetDiscountByIdQuery( Guid Id) : IRequest<DiscountDto>;