
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Activate
{
    public sealed record ActivateEducationServiceCommand(Guid Id) : IRequest<EducationServiceDto>;
}