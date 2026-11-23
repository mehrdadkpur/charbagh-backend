using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.GetAll
{
    public sealed class GetAllEducationServicesHandler : IRequestHandler<GetAllEducationServicesQuery,IReadOnlyList< EducationServiceDto>>
    {
        private readonly IEducationServiceRepository _repository;

        public GetAllEducationServicesHandler(IEducationServiceRepository repository)
        {
            _repository = repository;
        }

        public async Task<IReadOnlyList<EducationServiceDto>> Handle(GetAllEducationServicesQuery request, CancellationToken cancellationToken)
        {
            var educationServices = await _repository.GetAllAsync(cancellationToken);

            return educationServices.Select(x=> x.ToDto()).ToList();
        }
    }

}
