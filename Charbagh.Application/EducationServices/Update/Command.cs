using Charbagh.Application.EducationServices.Common;
using MediatR;

namespace Charbagh.Application.EducationServices.Update
{
    public sealed record UpdateEducationServiceCommand(
        Guid Id,
        string ServiceName,
        string? Description
        ) : IRequest<EducationServiceDto>;
}
