
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Activate
{
    public sealed class ActivateEducationServiceHandler : IRequestHandler<ActivateEducationServiceCommand, EducationServiceDto>
    {
        private readonly IEducationServiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateEducationServiceHandler(IEducationServiceRepository repository , IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EducationServiceDto> Handle(ActivateEducationServiceCommand request, CancellationToken cancellationToken)
        {
            var educationService = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (educationService == null)
            {
                throw new EducationServiceNotFoundException(request.Id);
            }

            educationService.Activate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return educationService.ToDto();
        }
    }

}
