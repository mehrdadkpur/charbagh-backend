
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.GetAll
{
    public sealed class GetAllCategoriesHandler : IRequestHandler<GetAllCategoriesQuery ,IReadOnlyList< CategoryDto>>
    {
        private readonly ICategoryRepository _repository;

        public GetAllCategoriesHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public async Task <IReadOnlyList<CategoryDto>> Handle (GetAllCategoriesQuery request ,  CancellationToken cancellationToken)
        {
            var categories = await _repository.GetAllAsync (cancellationToken);

            return categories.Select(x => x.ToDto ()).ToList();
        }
    }
}
