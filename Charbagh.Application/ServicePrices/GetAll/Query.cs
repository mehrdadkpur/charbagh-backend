
using Charbagh.Application.ServicePrices.Common;
using MediatR;

namespace Charbagh.Application.ServicePrices.GetAll
{
    public sealed record GetAllServicePricesQuery() : IRequest<IReadOnlyList< ServicePriceDto>>;
}
