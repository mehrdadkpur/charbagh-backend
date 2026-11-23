using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.GetById;

public sealed class GetDiscountTypeByIdHandler
    : IRequestHandler<
        GetDiscountTypeByIdQuery,
        DiscountTypeDto>
{
    private readonly IDiscountTypeRepository _repository;

    public GetDiscountTypeByIdHandler(
        IDiscountTypeRepository repository)
    {
        _repository = repository;
    }

    public async Task<DiscountTypeDto> Handle(
        GetDiscountTypeByIdQuery request,
        CancellationToken cancellationToken)
    {
        var discountType = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (discountType is null)
        {
            throw new KeyNotFoundException(
                "Discount type was not found.");
        }

        return discountType.ToDto();
    }
}