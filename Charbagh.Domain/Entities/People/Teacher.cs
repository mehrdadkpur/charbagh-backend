using Charbagh.Domain.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Domain.Entities.People;

public class Teacher : PersonProfile
{
    public string PersonnelCode { get; set; } = null!;

    public DateOnly EmploymentDate { get; set; }

    public UserEducation? Education { get; set; } = UserEducation.Illiterate;

    public int? ExperienceYears { get; set; } = 0;

    public string? Biography { get; set; }

    public bool IsVisibleOnWebsite { get; set; } = false;

    public TeacherStatus Status { get; set; } = TeacherStatus.Pending;

    public ICollection<TeacherInstrument> TeacherInstruments { get; set; }
        = new List<TeacherInstrument>();

    public ICollection<LessonAssignment> LessonAssignments { get; set; }
        = new List<LessonAssignment>();

    private Teacher()
    {
    }

    private Teacher(
        Guid userId,
        string personnelCode,
        DateOnly employmentDate)
    {
        UserId = userId;
        PersonnelCode = personnelCode;
        EmploymentDate = employmentDate;
        Status = TeacherStatus.Pending;
        IsVisibleOnWebsite = false;
    }

    public static Teacher Create(
        Guid userId,
        string personnelCode,
        DateOnly employmentDate)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException(
                "UserId is required.",
                nameof(userId));

        if (string.IsNullOrWhiteSpace(personnelCode))
            throw new ArgumentException(
                "Personnel code is required.",
                nameof(personnelCode));

        if (employmentDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException(
                "Employment date cannot be in the future.",
                nameof(employmentDate));

        var teacher = new Teacher(
            userId,
            personnelCode,
            employmentDate);

        teacher.MarkAsCreated();

        return teacher;
    }

    public void Update( DateOnly employmentDate)
    {
        if (employmentDate > DateOnly.FromDateTime(DateTime.UtcNow))
            throw new ArgumentException(
                "Employment date cannot be in the future.",
                nameof(employmentDate));
        EmploymentDate = employmentDate;

        MarkAsUpdated();
    }

    public void UpdateProfessionalInformation(
        UserEducation? education,
        int? experienceYears,
        string? biography)
    {
        if (experienceYears < 0)
            throw new ArgumentException(
                "Experience years cannot be negative.",
                nameof(experienceYears));

        Education = education;
        ExperienceYears = experienceYears;
        Biography = biography?.Trim();

        MarkAsUpdated();
    }

    public void Activate()
    {
        if (Status == TeacherStatus.Active)
            return;

        if (Status == TeacherStatus.Blocked)
            throw new InvalidOperationException(
                "Blocked Teacher cannot be activated directly.");

        Status = TeacherStatus.Active;
        MarkAsUpdated();
    }

    public void Suspend()
    {
        if (Status == TeacherStatus.Suspended)
            return;

        if (Status != TeacherStatus.Active)
            throw new InvalidOperationException(
                "Only an active Teacher can be suspended.");

        Status = TeacherStatus.Suspended;
        MarkAsUpdated();
    }

    public void Block()
    {
        if (Status == TeacherStatus.Blocked)
            return;

        Status = TeacherStatus.Blocked;
        MarkAsUpdated();
    }

    public void Unblock()
    {
        if (Status != TeacherStatus.Blocked)
            throw new InvalidOperationException(
                "Only a blocked Teacher can be unblocked.");

        Status = TeacherStatus.Active;
        MarkAsUpdated();
    }
}