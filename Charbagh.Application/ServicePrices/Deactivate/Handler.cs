
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Activate;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.Deactivate
{
    public sealed class DeactivateServicePriceHandler : IRequestHandler<DeactivateServicPriceCommand, ServicePriceDto>
    {
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateServicePriceHandler(IServicePriceRepository servicePriceRepository, IUnitOfWork unitOfWork)
        {
            _servicePriceRepository = servicePriceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePriceDto> Handle(DeactivateServicPriceCommand request, CancellationToken cancellationToken)
        {
            var servicePrice = await _servicePriceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (servicePrice == null)
            {
                throw new ServicePriceNotFoundException(request.Id);
            }

            servicePrice.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return servicePrice.ToDto();
        }
    }
}
