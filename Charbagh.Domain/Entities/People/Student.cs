using Charbagh.Domain.Common;
using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Domain.Entities.Finance;

namespace Charbagh.Domain.Entities.People
{
    public class Student : PersonProfile
    {
        public string StudentCode { get; private set; } = null!;

        public DateOnly RegistrationDate { get; private set; }

        public StudentStatus Status { get; private set; }

        public ICollection<LessonAssignment> LessonAssignments { get; private set; } = new List<LessonAssignment>();

        public ICollection<StudentParent> StudentParents { get; private set; } = new List<StudentParent>();

        public ICollection<Invoice> Invoices { get; private set; } = new List<Invoice>();

        private Student()
        {
        }

        private Student(Guid userId,string studentCode,DateOnly registrationDate)
        {
            UserId = userId;
            StudentCode = studentCode;
            RegistrationDate = registrationDate;
            Status = StudentStatus.Pending;

            MarkAsCreated();
        }

        public static Student Create(Guid userId, string studentCode, DateOnly registrationDate)
        {
            if (userId == Guid.Empty)
                throw new ArgumentException( "UserId is required.",  nameof(userId));

            if (string.IsNullOrWhiteSpace(studentCode))
                throw new ArgumentException( "Student code is required.", nameof(studentCode));

            if (registrationDate > DateOnly.FromDateTime(DateTime.UtcNow))
                throw new ArgumentException("Registration date cannot be in the future.", nameof(registrationDate));

            var student = new Student( userId, studentCode, registrationDate);

            student.MarkAsCreated();

            return student;
        }

        public void Update ( string studentCode, DateOnly registrationDate , string notes)
        {
            StudentCode = studentCode.Trim();
            RegistrationDate = registrationDate;
            Notes = notes.Trim();

            MarkAsUpdated();

        }

        public void Activate()
        {
            if (Status == StudentStatus.Active)
                return;
            if (Status == StudentStatus.Blocked)
            {
                throw new InvalidOperationException("Blocked student cannot be activated directly.");
            }

            Status = StudentStatus.Active;
            MarkAsUpdated();
        }

        public void Suspend()
        {
            if (Status == StudentStatus.Suspended)
                return;

            if (Status != StudentStatus.Active)
                throw new InvalidOperationException( "Only an active student can be suspended.");

            Status = StudentStatus.Suspended;
            MarkAsUpdated();
        }

        public void Block()
        {
            if (Status == StudentStatus.Blocked)
                return;

            Status = StudentStatus.Blocked;
            MarkAsUpdated();
        }

        public void Unblock()
        {
            if (Status != StudentStatus.Blocked)
                throw new InvalidOperationException( "Only a blocked student can be unblocked.");

            Status = StudentStatus.Active;
            MarkAsUpdated();
        }
    }
}

