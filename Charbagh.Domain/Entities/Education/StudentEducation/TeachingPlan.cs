using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Domain.Entities.Education.StudentEducation;

public sealed class TeachingPlan : BaseEntity
{
    public Guid LessonAssignmentId { get; private set; }

    public Guid BookId { get; private set; }

    public Guid StartBookItemId { get; private set; }

    public Guid? TargetBookItemId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly? EndDate { get; private set; }

    public TeachingPlanStatus Status { get; private set; }

    public int DisplayOrder { get; private set; }

    public string? Objective { get; private set; }

    public string? Notes { get; private set; }


    // Navigation Properties

    public LessonAssignment LessonAssignment { get; private set; } = null!;

    public Book Book { get; private set; } = null!;

    public BookItem StartBookItem { get; private set; } = null!;

    public BookItem? TargetBookItem { get; private set; }

    public ICollection<LessonContent> LessonContents { get; private set; } = new List<LessonContent>();


    private TeachingPlan()
    {
    }


    private TeachingPlan(
        Guid lessonAssignmentId,
        Guid bookId,
        Guid startBookItemId,
        Guid? targetBookItemId,
        DateOnly startDate,
        int displayOrder,
        string? objective,
        string? notes)
    {
        LessonAssignmentId = lessonAssignmentId;

        BookId = bookId;

        StartBookItemId = startBookItemId;

        TargetBookItemId = targetBookItemId;

        StartDate = startDate;

        DisplayOrder = displayOrder;

        Objective = objective?.Trim();

        Notes = notes?.Trim();

        Status = TeachingPlanStatus.Active;
    }


    public static TeachingPlan Create(
        Guid lessonAssignmentId,
        Guid bookId,
        Guid startBookItemId,
        Guid? targetBookItemId,
        DateOnly startDate,
        int displayOrder,
        string? objective,
        string? notes)
    {
        if (lessonAssignmentId == Guid.Empty)
            throw new ArgumentException(
                "Lesson assignment is required.",
                nameof(lessonAssignmentId));

        if (bookId == Guid.Empty)
            throw new ArgumentException(
                "Book is required.",
                nameof(bookId));

        if (startBookItemId == Guid.Empty)
            throw new ArgumentException(
                "Start book item is required.",
                nameof(startBookItemId));

        if (displayOrder < 0)
            throw new ArgumentException(
                "Display order cannot be negative.",
                nameof(displayOrder));

        var teachingPlan = new TeachingPlan(
            lessonAssignmentId,
            bookId,
            startBookItemId,
            targetBookItemId,
            startDate,
            displayOrder,
            objective,
            notes);

        teachingPlan.MarkAsCreated();

        return teachingPlan;
    }

        public void Update(
            Guid startBookItemId,
            Guid? targetBookItemId,
            DateOnly startDate,
            DateOnly? endDate,
            int displayOrder,
            string? objective,
            string? notes)
        {
                if (startBookItemId == Guid.Empty)
                    throw new ArgumentException(
                        "Start book item is required.",
                        nameof(startBookItemId));

                if (displayOrder < 0)
                    throw new ArgumentException(
                        "Display order cannot be negative.",
                        nameof(displayOrder));

                if (endDate.HasValue && endDate.Value < startDate)
                    throw new ArgumentException(
                        "End date cannot be earlier than start date.",
                        nameof(endDate));

                StartBookItemId = startBookItemId;
                TargetBookItemId = targetBookItemId;
                StartDate = startDate;
                EndDate = endDate;
                DisplayOrder = displayOrder;
                Objective = objective;
                Notes = notes;

                MarkAsUpdated();
            }


    public void Complete(DateOnly endDate)
    {
        if (Status != TeachingPlanStatus.Active)
        {
            throw new InvalidOperationException(
                "Only active Teaching Plans can be completed.");
        }

        if (endDate < StartDate)
        {
            throw new ArgumentException(
                "End date cannot be earlier than start date.",
                nameof(endDate));
        }

        EndDate = endDate;

        Status = TeachingPlanStatus.Completed;

        MarkAsUpdated();
    }


    public void Cancel(DateOnly endDate)
    {
        if (Status == TeachingPlanStatus.Completed)
        {
            throw new InvalidOperationException(
                "Completed Teaching Plans cannot be cancelled.");
        }

        if (endDate < StartDate)
        {
            throw new ArgumentException(
                "End date cannot be earlier than start date.",
                nameof(endDate));
        }

        EndDate = endDate;

        Status = TeachingPlanStatus.Cancelled;

        MarkAsUpdated();
    }

    public void Activate()
    {
        if (Status == TeachingPlanStatus.Active)
            return;

        Status = TeachingPlanStatus.Active;

        MarkAsUpdated();
    }
}