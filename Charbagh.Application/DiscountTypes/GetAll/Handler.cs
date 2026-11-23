using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.GetAll;

public sealed class GetAllDiscountTypesHandler
    : IRequestHandler<
        GetAllDiscountTypesQuery,
        IReadOnlyList<DiscountTypeDto>>
{
    private readonly IDiscountTypeRepository _repository;

    public GetAllDiscountTypesHandler(
        IDiscountTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<IReadOnlyList<DiscountTypeDto>> Handle(
        GetAllDiscountTypesQuery request,
        CancellationToken cancellationToken)
    {
        var discountTypes = await _repository.GetAllAsync(
            cancellationToken);

        return discountTypes
            .Select(x => x.ToDto())
            .ToList();
    }
}