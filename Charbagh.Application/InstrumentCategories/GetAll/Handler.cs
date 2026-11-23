using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.GetAll
{
    public sealed record GetAllInstrumentCategoriesHandler:IRequestHandler<GetAllInstrumentCategoriesQuery , IReadOnlyList<InstrumentCategoryDto>>
    {
        private readonly IInstrumentCategoryRepository _repository;

        public GetAllInstrumentCategoriesHandler(IInstrumentCategoryRepository repository)
        {
            _repository = repository;
        }
        public async Task<IReadOnlyList<InstrumentCategoryDto>> Handle(GetAllInstrumentCategoriesQuery query, CancellationToken cancellationToken)
        {
            var instrumentcategories = await _repository.GetAllAsync(cancellationToken);
            return instrumentcategories
                .Select(x => x.ToDto()).ToList();
        }
    }
}
