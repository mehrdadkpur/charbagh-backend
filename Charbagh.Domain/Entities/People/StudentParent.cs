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

    private StudentParent( Guid studentId, Guid parentId, StudentParentRelationship relationship)
    {
        StudentId = studentId;
        ParentId = parentId;
        Relationship = relationship;
    }

    public static StudentParent Create( Guid studentId, Guid parentId, StudentParentRelationship relationship)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException("Student is required.");

        if (parentId == Guid.Empty)
            throw new ArgumentException("Parent is required.");

        if (studentId == parentId)
            throw new InvalidOperationException(
                "Student and parent cannot be the same person.");

        var studentParent = new StudentParent( studentId,parentId, relationship);

        studentParent.MarkAsCreated();

        return studentParent;
    }

    public void UpdateRelationship( StudentParentRelationship relationship)
    {
        Relationship = relationship;

        MarkAsUpdated();
    }
}