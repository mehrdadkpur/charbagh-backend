
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Activate
{
    public sealed record ActivateStudentCommand(Guid Id) : IRequest<StudentDto>;
}
