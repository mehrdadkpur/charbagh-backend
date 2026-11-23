
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Suspend
{
    public sealed record SuspendTeacherCommand(Guid Id) : IRequest<TeacherDto>;
}
