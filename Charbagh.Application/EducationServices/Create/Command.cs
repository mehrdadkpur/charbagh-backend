

using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Create
{
    public sealed record CreateEducationServiceCommand(
       string ServiceName,
       string? Description
        ) : IRequest<EducationServiceDto>;
}
