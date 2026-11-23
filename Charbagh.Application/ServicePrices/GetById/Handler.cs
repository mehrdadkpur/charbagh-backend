using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.GetById
{
    public sealed class GetServicePriceByIdHandler : IRequestHandler<GetServicPriceByIdQuery , ServicePriceDto>
    {
        private readonly IServicePriceRepository _servicePriceRepository;

        public GetServicePriceByIdHandler(IServicePriceRepository servicePriceRepository)
        {
            _servicePriceRepository = servicePriceRepository;
        }

        public async Task<ServicePriceDto> Handle(GetServicPriceByIdQuery request , CancellationToken cancellationToken)
        {
            var servicePrice = await _servicePriceRepository.GetByIdAsync(request.Id , cancellationToken);

            if(servicePrice == null)
            {
                throw new ServicePriceNotFoundException(request.Id);
            }

            return servicePrice.ToDto();
        } 
    }
}
