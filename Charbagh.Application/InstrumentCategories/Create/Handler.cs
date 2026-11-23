using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Create;

public sealed class CreateInstrumentCategoryHandler : IRequestHandler<CreateInstrumentCategoryCommand, InstrumentCategoryDto>
{
    private readonly IInstrumentCategoryRepository _repository;
    private readonly IUnitOfWork _unitOfWork;

    public CreateInstrumentCategoryHandler(IInstrumentCategoryRepository repository,IUnitOfWork unitOfWork)
    {
        _repository = repository;
        _unitOfWork = unitOfWork;
    }

    public async Task<InstrumentCategoryDto> Handle(CreateInstrumentCategoryCommand request,CancellationToken cancellationToken)
    {
        var category = await _repository.GetByNameAsync( request.Name,cancellationToken);

        if (category is not null)
            throw new DuplicateInstrumentCategoryNameException(request.Name);

        category = InstrumentCategory.Create( request.Name, request.Description);

        await _repository.AddAsync( category, cancellationToken);

        await _unitOfWork.SaveChangesAsync(cancellationToken);

        return category.ToDto();
    }
}