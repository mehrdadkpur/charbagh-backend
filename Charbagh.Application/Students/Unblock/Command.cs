using Charbagh.Application.Students.Common;
using MediatR;

namespace Charbagh.Application.Students.Unblock
{
    public sealed record UnblockStudentCommand(Guid Id): IRequest<StudentDto>;    
}
