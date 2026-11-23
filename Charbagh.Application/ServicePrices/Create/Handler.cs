
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Common;
using Charbagh.Domain.Entities.Finance;
using MediatR;

namespace Charbagh.Application.ServicePrices.Create
{
    public sealed class CreateServicePriceHandler : IRequestHandler<CreateServicePriceCommand,ServicePriceDto>
    {
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly IEducationServiceRepository _educationServiceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateServicePriceHandler(IServicePriceRepository servicePriceRepository , IEducationServiceRepository educationServiceRepository , IUnitOfWork unitOfWork)
        {
            _servicePriceRepository = servicePriceRepository;
            _educationServiceRepository = educationServiceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePriceDto> Handle(CreateServicePriceCommand request , CancellationToken cancellationToken)
        {
            var educationService = await _educationServiceRepository.GetByIdAsync(request.EducationServiceId, cancellationToken);

            if (educationService == null)
            {
                throw new EducationServiceNotFoundException(request.EducationServiceId);
            }

            var exsitsSctiveServicePrice = await _servicePriceRepository.ExistsActiveByEducationServiceIdAsync(request.EducationServiceId , null, cancellationToken);

            if (exsitsSctiveServicePrice)
            {
                throw new DuplicateActiveServicePriceException(request.EducationServiceId);
            }

            var servicePrice = ServicePrice.Create(
                request.EducationServiceId,
                request.Price,
                request.EffectiveFrom
                );

            await _servicePriceRepository.AddAsync(servicePrice, cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return servicePrice.ToDto();

        }
    }
}
