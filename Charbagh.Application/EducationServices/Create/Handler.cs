
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.EducationServices.Common;
using Charbagh.Domain.Entities.Settings;
using MediatR;

namespace Charbagh.Application.EducationServices.Create
{
    public sealed class CreateEducationServiceHandler : IRequestHandler<CreateEducationServiceCommand , EducationServiceDto>
    {
        private readonly IEducationServiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateEducationServiceHandler(IEducationServiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EducationServiceDto> Handle(CreateEducationServiceCommand request , CancellationToken cancellationToken)
        {
            var educationService = EducationService.Create(request.ServiceName, request.Description);

            await _repository.AddAsync(educationService, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return educationService.ToDto();
        }
    }
}
