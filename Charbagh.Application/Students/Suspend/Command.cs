
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Suspend
{
    public sealed record SuspendStudentCommand(Guid Id) : IRequest<StudentDto>;
}
