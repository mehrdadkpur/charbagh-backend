using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.GetById
{
    public sealed record GetEducationServiceByIdQuery(Guid Id) : IRequest<EducationServiceDto>;
}
