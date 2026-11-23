using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.UpdatePrice
{
    public sealed record UpdatePriceServicePriceCommand(
        Guid Id,
        decimal Price
        ) : IRequest<ServicePriceDto>;
}
