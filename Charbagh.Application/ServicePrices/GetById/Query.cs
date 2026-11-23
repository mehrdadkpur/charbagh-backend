
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.GetById
{
    public sealed record GetServicPriceByIdQuery(Guid Id) : IRequest<ServicePriceDto>;
}
