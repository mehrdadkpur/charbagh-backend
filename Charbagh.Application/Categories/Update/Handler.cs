
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Categories.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Categories.Update
{
    public sealed class UpdateCategoryHandler : IRequestHandler<UpdateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(UpdateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            category.Update(request.Name , request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.ToDto();
        }
    }

}
