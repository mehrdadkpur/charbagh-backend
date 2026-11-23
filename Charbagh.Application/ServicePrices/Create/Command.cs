
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.Create
{
    public sealed record CreateServicePriceCommand(
        Guid EducationServiceId,
        decimal Price,
        DateOnly EffectiveFrom
        ) : IRequest<ServicePriceDto>;
}
