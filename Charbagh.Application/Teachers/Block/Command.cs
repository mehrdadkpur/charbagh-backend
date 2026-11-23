using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Block
{
    public sealed record BlockTeacherCommand(Guid Id) : IRequest<TeacherDto>;
}
