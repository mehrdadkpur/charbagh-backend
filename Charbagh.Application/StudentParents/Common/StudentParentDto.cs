
namespace Charbagh.Application.StudentParents.Common
{
    public sealed record StudentParentDto(Guid Id ,Guid StudentId, Guid ParentId , StudentParentRelationship Relationship);
}
