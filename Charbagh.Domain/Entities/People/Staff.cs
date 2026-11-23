using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.People;

public class Staff : PersonProfile
{
    public string PersonnelCode { get; private set; } = null!;

    public DateOnly EmploymentDate { get; private set; }

    public DateOnly? TerminationDate { get; private set; }

    public EmploymentType EmploymentType { get; private set; }

    public StaffStatus Status { get; private set; }

    private Staff()
    {

    }

    private Staff(Guid userId, string personnelCode, DateOnly employmentDate, EmploymentType employmentType)
    {
        UserId = userId;
        PersonnelCode = personnelCode;
        EmploymentDate = employmentDate;
        EmploymentType = employmentType;
        Status = StaffStatus.Active;
    }

    public static Staff Create(Guid userId, string personnelCode, DateOnly employmentDate, EmploymentType employmentType)
    {
        if (userId == Guid.Empty)
            throw new ArgumentException("UserId is required.", nameof(userId));

        if (string.IsNullOrWhiteSpace(personnelCode))
            throw new ArgumentException("Personnel code is required.", nameof(personnelCode));

        var staff = new Staff(userId, personnelCode, employmentDate, employmentType);

        staff.MarkAsCreated();

        return staff;
    }

    public void Update( DateOnly employmentDate, EmploymentType employmentType)
    {

        if (Status == StaffStatus.Terminated)
            throw new InvalidOperationException("Terminated staff cannot be updated.");

        EmploymentDate = employmentDate;
        EmploymentType = employmentType;

        MarkAsUpdated();
    }

    public void Terminate(DateOnly terminationDate)
    {
        if (Status == StaffStatus.Terminated)
        {
            throw new InvalidOperationException("Staff is already terminated.");
        }

        if (terminationDate < EmploymentDate)
        {
            throw new InvalidOperationException( "Termination date cannot be earlier than employment date.");
        }

        TerminationDate = terminationDate;

        Status = StaffStatus.Terminated;

        MarkAsUpdated();
    }

    public void Activate()
    {
        if (Status == StaffStatus.Terminated)
        {
            throw new InvalidOperationException("Terminated staff cannot be activated.");
        }

        if (Status == StaffStatus.Active)
            return;

        Status = StaffStatus.Active;

        MarkAsUpdated();
    }

    public void PutOnLeave()
    {
        if (Status == StaffStatus.Terminated)
        {
            throw new InvalidOperationException( "Terminated staff cannot be put on leave.");
        }

        if (Status == StaffStatus.OnLeave)
            return;

        Status = StaffStatus.OnLeave;

        MarkAsUpdated();
    }


}