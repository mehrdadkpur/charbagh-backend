using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.GetAll;

public sealed record GetAllDiscountsQuery : IRequest<IReadOnlyList<DiscountDto>>;