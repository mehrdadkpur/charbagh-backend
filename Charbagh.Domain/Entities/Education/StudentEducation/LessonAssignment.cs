using Charbagh.Domain.Entities.Education.EducationCatalog;
using Charbagh.Domain.Entities.People;

namespace Charbagh.Domain.Entities.Education.StudentEducation;

public sealed class LessonAssignment : BaseEntity
{
    public Guid StudentId { get; private set; }

    public Guid BookId { get; private set; }

    public Guid TeacherId { get; private set; }

    public Guid InstrumentId { get; private set; }

    public DateOnly StartDate { get; private set; }

    public DateOnly? EndDate { get; private set; }

    public LessonAssignmentStatus Status { get; private set; }

    public decimal SessionFee { get; private set; }

    public PaymentPlanType PaymentPlan { get; private set; }

    public string? Notes { get; private set; }

    public bool IsActive =>
        Status == LessonAssignmentStatus.Active;


    // Navigation Properties

    public Student Student { get; private set; } = null!;

    public Teacher Teacher { get; private set; } = null!;

    public Instrument Instrument { get; private set; } = null!;

    public Book Book { get; private set; } = null!;

    public ICollection<LessonSession> LessonSessions { get; private set; } = new List<LessonSession>();
    public ICollection<TeachingPlan> TeachingPlans { get; private set; } = new List<TeachingPlan>();

    private LessonAssignment()
    {
    }


    private LessonAssignment(
        Guid studentId,
        Guid bookId,
        Guid teacherId,
        Guid instrumentId,
        DateOnly startDate,
        decimal sessionFee,
        PaymentPlanType paymentPlan,
        string? notes)
    {
        StudentId = studentId;

        BookId = bookId;

        TeacherId = teacherId;

        InstrumentId = instrumentId;

        StartDate = startDate;

        SessionFee = sessionFee;

        PaymentPlan = paymentPlan;

        Notes = notes?.Trim();

        Status = LessonAssignmentStatus.Active;
    }


    public static LessonAssignment Create(
        Guid studentId,
        Guid bookId,
        Guid teacherId,
        Guid instrumentId,
        DateOnly startDate,
        decimal sessionFee,
        PaymentPlanType paymentPlan,
        string? notes)
    {
        ValidateIds(studentId, bookId, teacherId, instrumentId);

        if (sessionFee < 0)
        {
            throw new ArgumentException(
                "Session fee cannot be negative.",
                nameof(sessionFee));
        }

        var lessonAssignment = new LessonAssignment(
            studentId,
            bookId,
            teacherId,
            instrumentId,
            startDate,
            sessionFee,
            paymentPlan,
            notes);

        lessonAssignment.MarkAsCreated();

        return lessonAssignment;
    }


    public void Update(
        Guid teacherId,
        Guid bookId,
        decimal sessionFee,
        PaymentPlanType paymentPlan,
        string? notes)
    {
        if (teacherId == Guid.Empty)
        {
            throw new ArgumentException(
                "Teacher is required.",
                nameof(teacherId));
        }

        if (bookId == Guid.Empty)
        {
            throw new ArgumentException(
                "Book is required.",
                nameof(bookId));
        }

        if (sessionFee < 0)
        {
            throw new ArgumentException(
                "Session fee cannot be negative.",
                nameof(sessionFee));
        }

        TeacherId = teacherId;

        BookId = bookId;

        SessionFee = sessionFee;

        PaymentPlan = paymentPlan;

        Notes = notes?.Trim();

        MarkAsUpdated();
    }


    public void Suspend()
    {
        if (Status != LessonAssignmentStatus.Active)
        {
            throw new InvalidOperationException(
                "Only active lesson assignments can be suspended.");
        }

        Status = LessonAssignmentStatus.Suspended;

        MarkAsUpdated();
    }


    public void Resume()
    {
        if (Status != LessonAssignmentStatus.Suspended)
        {
            throw new InvalidOperationException(
                "Only suspended lesson assignments can be resumed.");
        }

        Status = LessonAssignmentStatus.Active;

        MarkAsUpdated();
    }


    public void Complete(DateOnly endDate)
    {
        if (Status != LessonAssignmentStatus.Active)
        {
            throw new InvalidOperationException(
                "Only active lesson assignments can be completed.");
        }

        if (endDate < StartDate)
        {
            throw new ArgumentException(
                "End date cannot be earlier than start date.",
                nameof(endDate));
        }

        EndDate = endDate;

        Status = LessonAssignmentStatus.Completed;

        MarkAsUpdated();
    }


    public void Cancel(DateOnly endDate)
    {
        if (Status == LessonAssignmentStatus.Completed)
        {
            throw new InvalidOperationException(
                "Completed lesson assignment cannot be cancelled.");
        }

        if (endDate < StartDate)
        {
            throw new ArgumentException(
                "End date cannot be earlier than start date.",
                nameof(endDate));
        }

        EndDate = endDate;

        Status = LessonAssignmentStatus.Cancelled;

        MarkAsUpdated();
    }


    private static void ValidateIds(
        Guid studentId,
        Guid bookId,
        Guid teacherId,
        Guid instrumentId)
    {
        if (studentId == Guid.Empty)
            throw new ArgumentException(
                "Student is required.",
                nameof(studentId));

        if (bookId == Guid.Empty)
            throw new ArgumentException(
                "Book is required.",
                nameof(bookId));

        if (teacherId == Guid.Empty)
            throw new ArgumentException(
                "Teacher is required.",
                nameof(teacherId));

        if (instrumentId == Guid.Empty)
            throw new ArgumentException(
                "Instrument is required.",
                nameof(instrumentId));
    }
}