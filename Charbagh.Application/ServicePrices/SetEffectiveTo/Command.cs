using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.SetEffectiveTo
{
    public sealed record SetEffectiveToServicPriceCommand(Guid Id , DateOnly EffectiveTo) : IRequest<ServicePriceDto>;
}
