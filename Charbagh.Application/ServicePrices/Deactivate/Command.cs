using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.Deactivate
{
    public sealed record DeactivateServicPriceCommand(Guid Id) : IRequest<ServicePriceDto>;
}
