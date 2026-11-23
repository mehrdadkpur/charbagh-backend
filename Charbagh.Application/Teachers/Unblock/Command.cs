using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Unblock
{
    public sealed record UnblockTeacherCommand(Guid Id) : IRequest<TeacherDto>;
}
