

using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Create
{
    public sealed record CreateEducationServiceCommand(
       Guid Id,
       string ServiceName,
       string? Description
        ) : IRequest<EducationServiceDto>;
}
