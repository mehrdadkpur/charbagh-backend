
using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Deactivate
{
    public sealed record DeactivateEducationServiceCommand(Guid Id) : IRequest<EducationServiceDto>;
}
