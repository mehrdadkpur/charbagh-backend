using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.DiscountTypes.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.DiscountTypes.Create;

public sealed class CreateDiscountTypeHandler
    : IRequestHandler<
        CreateDiscountTypeCommand,
        DiscountTypeDto>
{
    private readonly IDiscountTypeRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateDiscountTypeHandler(
        IDiscountTypeRepository repository,
        IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<DiscountTypeDto> Handle(
        CreateDiscountTypeCommand request,
        CancellationToken cancellationToken)
    {
        var exists = await _repository.ExistsByNameAsync(
            request.Name,
            cancellationToken);

        if (exists)
        {
            throw new InvalidOperationException(
                "A discount type with this name already exists.");
        }

        var discountType = DiscountType.Create(
            request.Name,
            request.Description,
            request.DisplayOrder);

        await _repository.AddAsync(
            discountType,
            cancellationToken);

        await _unitOfWork.SaveChangesAsync(
            cancellationToken);

        return discountType.ToDto();
    }
}