
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.Activate
{
    public sealed record ActivateServicPriceCommand(Guid Id) : IRequest<ServicePriceDto>;
}
