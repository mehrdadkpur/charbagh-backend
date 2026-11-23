using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.InstrumentCategories.Common;
using MediatR;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Charbagh.Application.InstrumentCategories.Deactivate
{
    public sealed record DeactivateInstrumentCategoryHandler:IRequestHandler<DeactivateInstrumentCategoryCommand , InstrumentCategoryDto>
    {
        private readonly IInstrumentCategoryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateInstrumentCategoryHandler (IInstrumentCategoryRepository instrumentCategoryRepository , IUnitOfWork unitOfWork) 
        {
            _repository = instrumentCategoryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task <InstrumentCategoryDto> Handle (DeactivateInstrumentCategoryCommand request , CancellationToken cancellationToken)
        {
            var instrumentCategory = await _repository.GetByIdAsync(request.Id, cancellationToken);
            if (instrumentCategory == null)
            {
                throw new InstrumentCategoryNotFoundException(request.Id);
            }
            if (instrumentCategory.IsActive)
            {
                instrumentCategory.Deactivate();

                await _unitOfWork.SaveChangesAsync(cancellationToken);
            }

            return instrumentCategory.ToDto();
        }
    }
}
