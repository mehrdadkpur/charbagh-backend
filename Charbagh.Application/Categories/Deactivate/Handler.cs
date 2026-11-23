using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Categories.Activate;
using Charbagh.Application.Categories.Common;
using Charbagh.Application.Common.Exceptions;
using MediatR;

namespace Charbagh.Application.Categories.Deactivate
{
    public sealed class DeactivateCategoryHandler : IRequestHandler<DeactivateCategoryCommand, CategoryDto>
    {
        private readonly ICategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateCategoryHandler(ICategoryRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<CategoryDto> Handle(DeactivateCategoryCommand request, CancellationToken cancellationToken)
        {
            var category = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (category == null)
            {
                throw new CategoryNotFoundException(request.Id);
            }

            category.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return category.ToDto();
        }
    }

}
