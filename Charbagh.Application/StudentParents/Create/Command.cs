
using Charbagh.Application.StudentParents.Common;
using MediatR;

namespace Charbagh.Application.StudentParents.Create
{
    public sealed record CreateStudentParentCommand(Guid Id ,Guid StudentId , Guid ParentId , StudentParentRelationship Relationship) : IRequest<StudentParentDto>;
}
