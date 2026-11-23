using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetByStudentId
{
    public sealed record GetStudentParentByStudentIdQuery(Guid studentId):IRequest<StudentParentDto>;
}
