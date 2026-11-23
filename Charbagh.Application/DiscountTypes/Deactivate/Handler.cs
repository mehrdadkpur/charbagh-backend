using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Deactivate;

public sealed class DeactivateDiscountTypeHandler
    : IRequestHandler<
        DeactivateDiscountTypeCommand,
        DiscountTypeDto>
{
    private readonly IDiscountTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public DeactivateDiscountTypeHandler(
        IDiscountTypeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiscountTypeDto> Handle(
        DeactivateDiscountTypeCommand request,
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

        discountType.Deactivate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return discountType.ToDto();
    }
}