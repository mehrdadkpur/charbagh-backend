
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.UpdatePrice
{
    public sealed class UpdatePriceServicePriceHandler : IRequestHandler<UpdatePriceServicePriceCommand , ServicePriceDto> 
    {
        private readonly IServicePriceRepository _servicePriceRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdatePriceServicePriceHandler(IServicePriceRepository servicePriceRepository, IUnitOfWork unitOfWork)
        {
            _servicePriceRepository = servicePriceRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<ServicePriceDto> Handle(UpdatePriceServicePriceCommand request , CancellationToken cancellationToken)
        {
            var servicePrice = await _servicePriceRepository.GetByIdAsync(request.Id , cancellationToken);

            if (servicePrice == null)
            {
                throw new ServicePriceNotFoundException(request.Id);
            }

            servicePrice.UpdatePrice(request.Price);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return servicePrice.ToDto();
        }
    }
}
