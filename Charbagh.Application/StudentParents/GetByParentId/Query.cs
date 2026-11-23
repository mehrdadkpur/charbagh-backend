
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.GetByParentId
{
    public sealed record GetStudentParentByParentIdQuery(Guid ParentId):IRequest<StudentParentDto>;
}
