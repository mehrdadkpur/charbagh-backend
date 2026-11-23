using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Discounts.Common;
using Charbagh.Application.Discounts.GetAll;
using MediatR;

namespace Charbagh.Application.Features.Finance.Discounts.GetAllDiscounts;

public sealed class GetAllDiscountsHandler
    : IRequestHandler<
        GetAllDiscountsQuery,
        IReadOnlyList<DiscountDto>>
{
    private readonly IDiscountRepository _repository;

    public GetAllDiscountsHandler(
        IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DiscountDto>> Handle(
        GetAllDiscountsQuery request,
        CancellationToken cancellationToken)
    {
        var discounts = await _repository.GetAllAsync(
            cancellationToken);

        return discounts
            .Select(x => x.ToDto())
            .ToList();
    }
}