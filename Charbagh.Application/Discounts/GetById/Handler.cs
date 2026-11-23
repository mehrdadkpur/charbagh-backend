using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Discounts.Common;
using MediatR;

namespace Charbagh.Application.Discounts.GetById;

public sealed class GetDiscountByIdHandler
    : IRequestHandler<GetDiscountByIdQuery, DiscountDto>
{
    private readonly IDiscountRepository _repository;

    public GetDiscountByIdHandler(
        IDiscountRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiscountDto> Handle(
        GetDiscountByIdQuery request,
        CancellationToken cancellationToken)
    {
        var discount = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (discount is null)
        {
            throw new InvalidOperationException(
                "Discount was not found.");
        }

        return discount.ToDto();
    }
}