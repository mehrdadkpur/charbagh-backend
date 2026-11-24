using Charbagh.Domain.Entities.People;

public class StudentParent : BaseEntity
{
    public Guid StudentId { get; private set; }

    public Guid ParentId { get; private set; }

    public StudentParentRelationship Relationship { get; private set; }

    public Student Student { get; private set; } = null!;

    public Parent Parent { get; private set; } = null!;

    private StudentParent()
    {
    }

    private StudentParent(Guid studentId, Guid parentId, StudentParentRelationship relationship)
    {
        StudentId = studentId;
        ParentId = parentId;
        Relationship = relationship;
    }

    public static StudentParent Create(Guid studentId, Guid parentId, StudentParentRelationship relationship)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student is required.", nameof(studentId));

        if (parentId == Guid.Empty)
            throw new ArgumentException("Parent is required.", nameof(parentId));

        if (studentId == parentId)
            throw new InvalidOperationException("Student and parent cannot be the same person.");

        if (!Enum.IsDefined(typeof(StudentParentRelationship), relationship))
            throw new ArgumentOutOfRangeException(nameof(relationship), "Student-parent relationship is not valid.");

        var studentParent = new StudentParent(studentId, parentId, relationship);
        studentParent.MarkAsCreated();

        return studentParent;
    }

    public void UpdateRelationship(StudentParentRelationship relationship)
    {
        if (!Enum.IsDefined(typeof(StudentParentRelationship), relationship))
            throw new ArgumentOutOfRangeException(nameof(relationship), "Student-parent relationship is not valid.");

        Relationship = relationship;
        MarkAsUpdated();
    }
}
