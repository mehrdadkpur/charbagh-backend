using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Update
{
    public sealed record UpdateInstrumentCategoryHandler : IRequestHandler<UpdateInstrumentCategoryCommand , InstrumentCategoryDto>
    {
        private readonly IInstrumentCategoryRepository _category;
        private readonly IUnitOfWork _unitOfWork;
    
        public UpdateInstrumentCategoryHandler(IInstrumentCategoryRepository category, IUnitOfWork unitOfWork)
        {
            _category = category;
            _unitOfWork = unitOfWork;
        }

        public async Task<InstrumentCategoryDto> Handle(UpdateInstrumentCategoryCommand request , CancellationToken cancellationToken)
        {
            var instrumentCategory = await _category.GetByIdAsync(request.Id , cancellationToken);

            if (instrumentCategory == null)
            {
                throw new InstrumentCategoryNotFoundException(request.Id);
            }

            var exiting = await _category.GetByNameAsync(request.Name, cancellationToken);
            if (exiting is not null && exiting.Id != request.Id)
            {
                throw new DuplicateInstrumentCategoryNameException(request.Name);
            }

            instrumentCategory.Update(request.Name , request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);
            return instrumentCategory.ToDto();
        }
    }

}
