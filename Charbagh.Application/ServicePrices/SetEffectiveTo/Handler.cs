using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.SetEffectiveTo
{
    public sealed class SetEffectiveToServicePriceHandler : IRequestHandler<SetEffectiveToServicPriceCommand, ServicePriceDto>
    {
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public SetEffectiveToServicePriceHandler(IServicePriceRepository servicePriceRepository, IUnitOfWork unitOfWork)
        {
            _servicePriceRepository = servicePriceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePriceDto> Handle(SetEffectiveToServicPriceCommand request, CancellationToken cancellationToken)
        {
            var servicePrice = await _servicePriceRepository.GetByIdAsync(request.Id, cancellationToken);

            if (servicePrice == null)
            {
                throw new ServicePriceNotFoundException(request.Id);
            }

            servicePrice.SetEffectiveTo(request.EffectiveTo);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return servicePrice.ToDto();
        }
    }
}
