
using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Update
{
    public sealed record UpdateStudentCommand(
        Guid UserId , 
        string StudentCode , 
        DateOnly RegistrationDate , 
        StudentStatus Status , 
        Guid Id , 
        string Notes 
        ) : IRequest<StudentDto>;
}
