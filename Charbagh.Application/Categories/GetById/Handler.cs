
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Categories.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Categories.GetById
{
    public sealed class GetCategoryByIdHandler : IRequestHandler<GetCategoryByIdQuery , CategoryDto>
    {
        private readonly ICategoryRepository _repository;

        public GetCategoryByIdHandler(ICategoryRepository repository)
        {
            _repository = repository;
        }

        public  async Task<CategoryDto> Handle(GetCategoryByIdQuery request , CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            return category.ToDto();
        }
    }
}
