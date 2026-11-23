using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.GetAll;

public sealed record GetAllDiscountTypesQuery : IRequest<IReadOnlyList<DiscountTypeDto>>;