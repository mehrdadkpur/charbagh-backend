
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.UpdateRelationship
{
    public sealed record UpdateStudentParentRelationCommand(Guid Id ,StudentParentRelationship Relationship): IRequest<StudentParentDto>;
}
