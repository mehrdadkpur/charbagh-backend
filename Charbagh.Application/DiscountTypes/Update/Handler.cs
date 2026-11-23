using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Update;

public sealed class UpdateDiscountTypeHandler
    : IRequestHandler<
        UpdateDiscountTypeCommand,
        DiscountTypeDto>
{
    private readonly IDiscountTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public UpdateDiscountTypeHandler(
        IDiscountTypeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiscountTypeDto> Handle(
        UpdateDiscountTypeCommand request,
        CancellationToken cancellationToken)
    {
        var discountType = await _repository.GetForUpdateAsync(
            request.Id,
            cancellationToken);

        if (discountType is null)
        {
            throw new KeyNotFoundException(
                "Discount type was not found.");
        }

        if (!string.Equals(
                discountType.Name,
                request.Name,
                StringComparison.OrdinalIgnoreCase))
        {
            var exists = await _repository.ExistsByNameAsync(
                request.Name,
                cancellationToken);

            if (exists)
            {
                throw new InvalidOperationException(
                    "A discount type with this name already exists.");
            }
        }

        discountType.Update(
            request.Name,
            request.Description,
            request.DisplayOrder);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return discountType.ToDto();
    }
}