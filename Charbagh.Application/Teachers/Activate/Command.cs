using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Activate
{
    public sealed record ActivateTeacherCommand(Guid Id) : IRequest<TeacherDto>;
}
