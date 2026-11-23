using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.Activate
{
    public sealed class ActivateServicePriceHandler : IRequestHandler<ActivateServicPriceCommand, ServicePriceDto>
    {
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ActivateServicePriceHandler(IServicePriceRepository servicePriceRepository , IUnitOfWork unitOfWork)
        {
            _servicePriceRepository = servicePriceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePriceDto> Handle(ActivateServicPriceCommand request, CancellationToken cancellationToken)
        {
            var servicePrice = await _servicePriceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (servicePrice == null)
            {
                throw new ServicePriceNotFoundException(request.Id);
            }

            servicePrice.Activate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return servicePrice.ToDto();
        }
    }
}
