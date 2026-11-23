
namespace Charbagh.Application.StudentParents.Common
{
    public static class StudentParentMapper
    {
        public static StudentParentDto ToDto(this StudentParent studentParent)
        {
            return new StudentParentDto(studentParent.Id,studentParent.StudentId, studentParent.ParentId , studentParent.Relationship);
        }
    }
}
