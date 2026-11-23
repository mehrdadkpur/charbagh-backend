using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.GetById
{
    public sealed class GetEducationServiceByIdHandler:IRequestHandler<GetEducationServiceByIdQuery , EducationServiceDto>
    {
        private readonly IEducationServiceRepository _repository;

        public GetEducationServiceByIdHandler(IEducationServiceRepository repository) 
        {
            _repository = repository;
        }

        public async Task<EducationServiceDto> Handle(GetEducationServiceByIdQuery request , CancellationToken cancellationToken)
        {
            var educationService = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (educationService == null)
            {
                throw new EducationServiceNotFoundException(request.Id);
            }

            return educationService.ToDto();
        }
    }
}
