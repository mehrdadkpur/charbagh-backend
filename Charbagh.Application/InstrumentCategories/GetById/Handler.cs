using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.GetById;

public sealed class GetInstrumentCategoryByIdHandler : IRequestHandler<GetInstrumentCategoryByIdQuery, InstrumentCategoryDto>
{
    private readonly IInstrumentCategoryRepository _repository;

    public GetInstrumentCategoryByIdHandler( IInstrumentCategoryRepository repository)
    {
        _repository = repository;
    }

    public async Task<InstrumentCategoryDto> Handle( GetInstrumentCategoryByIdQuery request,
        CancellationToken cancellationToken)
    {
        var category = await _repository.GetByIdAsync(
            request.Id,
            cancellationToken);

        if (category is null)
            throw new InstrumentCategoryNotFoundException(request.Id);

        return category.ToDto();
    }
}