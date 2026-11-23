
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.GetAll
{
    public sealed record GetAllEducationServicesQuery() : IRequest< IReadOnlyList<EducationServiceDto>>;
}
