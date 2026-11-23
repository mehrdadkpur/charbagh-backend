
using Charbagh.Application.Teachers.Common;
using MediatR;

namespace Charbagh.Application.Teachers.Create
{
    public sealed record CreateTeacherCommand( Guid UserId, DateOnly EmploymentDate) :IRequest<TeacherDto>;
 
}
