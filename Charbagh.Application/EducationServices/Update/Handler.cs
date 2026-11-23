using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Update
{
    public sealed class UpdateEducationServiceHandler : IRequestHandler<UpdateEducationServiceCommand, EducationServiceDto>
    {
        private readonly IEducationServiceRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateEducationServiceHandler(IEducationServiceRepository repository, IUnitOfWork unitOfWork)
        {
            _repository = repository;
            _unitOfWork = unitOfWork;
        }

        public async Task<EducationServiceDto> Handle(UpdateEducationServiceCommand request, CancellationToken cancellationToken)
        {
            var educationService = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (educationService == null)
            {
                throw new EducationServiceNotFoundException(request.Id);
            }

            educationService.Update( request.ServiceName,request.Description);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return educationService.ToDto();
        }
    }
}
