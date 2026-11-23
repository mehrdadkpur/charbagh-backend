
namespace Charbagh.Domain.Entities.Branches;

public sealed class Classroom : BaseEntity
{
    public Guid BranchId { get; private set; }

    public string Name { get; private set; } = null!;

    public string Code { get; private set; } = null!;

    public int Capacity { get; private set; }

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }


    // Navigation Properties

    public Branch Branch { get; private set; } = null!;


    // EF Core
    private Classroom()
    {
    }


    private Classroom(
        Guid branchId,
        string name,
        string code,
        int capacity,
        string? description)
    {
        BranchId = branchId;
        Name = name.Trim();
        Code = code.Trim();
        Capacity = capacity;
        Description = description?.Trim();
        IsActive = true;
    }


    public static Classroom Create(
        Guid branchId,
        string name,
        string code,
        int capacity,
        string? description = null)
    {
        ValidateBranchId(branchId);
        ValidateName(name);
        ValidateCode(code);
        ValidateCapacity(capacity);

        var classroom = new Classroom(
            branchId,
            name,
            code,
            capacity,
            description);

        classroom.MarkAsCreated();

        return classroom;
    }


    public void Update(
        string name,
        string code,
        int capacity,
        string? description)
    {
        ValidateName(name);
        ValidateCode(code);
        ValidateCapacity(capacity);

        Name = name.Trim();
        Code = code.Trim();
        Capacity = capacity;
        Description = description?.Trim();

        MarkAsUpdated();
    }


    public void Activate()
    {
        if (IsActive)
            return;

        IsActive = true;

        MarkAsUpdated();
    }


    public void Deactivate()
    {
        if (!IsActive)
            return;

        IsActive = false;

        MarkAsUpdated();
    }


    private static void ValidateBranchId(Guid branchId)
    {
        if (branchId == Guid.Empty)
            throw new ArgumentException(
                "Branch id is required.",
                nameof(branchId));
    }


    private static void ValidateName(string name)
    {
        if (string.IsNullOrWhiteSpace(name))
            throw new ArgumentException(
                "Classroom name is required.",
                nameof(name));
    }


    private static void ValidateCode(string code)
    {
        if (string.IsNullOrWhiteSpace(code))
            throw new ArgumentException(
                "Classroom code is required.",
                nameof(code));
    }


    private static void ValidateCapacity(int capacity)
    {
        if (capacity <= 0)
            throw new ArgumentException(
                "Classroom capacity must be greater than zero.",
                nameof(capacity));
    }
}