
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;

namespace Charbagh.Application.InstrumentCategories.Activate
{
    public sealed record ActivateInstrumentCategoryHandler : IRequestHandler<ActivateInstrumentCategoryCommand, InstrumentCategoryDto>
    {
        private readonly IInstrumentCategoryRepository _instrumentCategoryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateInstrumentCategoryHandler(IInstrumentCategoryRepository instrumentCategoryRepository, IUnitOfWork unitOfWork)
        {
            _instrumentCategoryRepository = instrumentCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<InstrumentCategoryDto> Handle(ActivateInstrumentCategoryCommand request , CancellationToken cancellationToken)
        {
            var instrumentCategory = await _instrumentCategoryRepository.GetByIdAsync(request.Id , cancellationToken);

            if (instrumentCategory == null)
            {
                throw new InstrumentCategoryNotFoundException(request.Id);
            }

            if (!instrumentCategory.IsActive)
            { 
                instrumentCategory.Activate();
                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }
            return instrumentCategory.ToDto();
        }
    }
}
