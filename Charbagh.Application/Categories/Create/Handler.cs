
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Categories.Common;
using MediatR;

namespace Charbagh.Application.Categories.Create
{
    public sealed class CreateCategoryHandler : IRequestHandler<CreateCategoryCommand , CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateCategoryHandler(ICategoryRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(CreateCategoryCommand request , CancellationToken cancellationToken)
        {
            var category = Category.Create(request.Name, request.Description);

            await _repository.AddAsync(category , cancellationToken);
            
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.ToDto();           
        }
    }
}
