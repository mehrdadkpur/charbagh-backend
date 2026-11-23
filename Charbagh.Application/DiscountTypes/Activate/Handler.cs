using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Activate;

public sealed class ActivateDiscountTypeHandler
    : IRequestHandler<
        ActivateDiscountTypeCommand,
        DiscountTypeDto>
{
    private readonly IDiscountTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public ActivateDiscountTypeHandler(
        IDiscountTypeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiscountTypeDto> Handle(
        ActivateDiscountTypeCommand request,
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

        discountType.Activate();

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return discountType.ToDto();
    }
}