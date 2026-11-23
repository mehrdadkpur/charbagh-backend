using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Update
{
    public sealed record UpdateTeacherCommand(
        Guid Id,
        Guid UserId,
        DateOnly EmploymentDate
        ): IRequest<TeacherDto>;
    
}
