using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.GetAll
{
    public sealed class GetAllServicePricesHandler : IRequestHandler<GetAllServicePricesQuery,IReadOnlyList< ServicePriceDto>>
    {
        private readonly IServicePriceRepository _servicePriceRepository;

        public GetAllServicePricesHandler(IServicePriceRepository servicePriceRepository)
        {
            _servicePriceRepository = servicePriceRepository;
        }

        public async Task<IReadOnlyList< ServicePriceDto>> Handle(GetAllServicePricesQuery request, CancellationToken cancellationToken)
        {
            var servicePrices = await _servicePriceRepository.GetAllAsync(cancellationToken);

            return servicePrices.Select(x => x.ToDto()).ToList();
        }
    }
}
