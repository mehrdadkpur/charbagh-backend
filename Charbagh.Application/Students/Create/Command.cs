using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Create
{
    public sealed record CreateStudentCommand(Guid UserId, DateOnly RegistrationDate) :IRequest<StudentDto>;
}
