using Charbagh.Domain.Common;
using Charbagh.Domain.Entities.Identity;

namespace Charbagh.Domain.Entities.Education.StudentEducation
{
    public sealed class Attendance : BaseEntity
    {
        public Guid LessonSessionId { get; private set; }

        public AttendanceStatus Status { get; private set; }

        public DateTime? CheckInAt { get; private set; }

        public DateTime? CheckOutAt { get; private set; }

        public string? Reason { get; private set; }

        public string? Note { get; private set; }

        public Guid? RecordedByUserId { get; private set; }

        public DateTime RecordedAt { get; private set; }

        // Navigation Properties
        public LessonSession LessonSession { get; private set; } = null!;

        public User? RecordedByUser { get; private set; }

        private Attendance() { }

        private Attendance(
            Guid lessonSessionId,
            AttendanceStatus status,
            DateTime? checkInAt,
            DateTime? checkOutAt,
            string? reason,
            string? note,
            Guid? recordedByUserId

            )
        {
            LessonSessionId = lessonSessionId;
            Status = status;
            CheckInAt = checkInAt;
            CheckOutAt = checkOutAt;
            Reason = reason;
            Note = note;
            RecordedByUserId = recordedByUserId;
            RecordedAt = DateTime.UtcNow;
        }

        public static Attendance Create(
            Guid lessonSessionId,
            AttendanceStatus status,
            DateTime? checkInAt,
            DateTime? checkOutAt,
            string? reason,
            string? note,
            Guid? recordedByUserId
            )
        {
            if (lessonSessionId == Guid.Empty)
                throw new ArgumentException("Lesson session is required.", nameof(lessonSessionId));

            var attendance = new Attendance(
                lessonSessionId,
                status,
                checkInAt,
                checkOutAt,
                reason,
                note,
                recordedByUserId
                );
            attendance.MarkAsCreated();
            return attendance;
        }

        public void Update(
            DateTime? checkInAt,
            DateTime? checkOutAt,
            string? reason,
            string? note
            )
        {
            ValidateTimes(checkInAt, checkOutAt);

            CheckInAt = checkInAt;
            CheckOutAt = checkOutAt;
            Reason = reason;
            Note = note;

            MarkAsUpdated();
        }

        public void SetPresent()
        {
            if (Status == AttendanceStatus.Present)
            {
                return;
            }
            Status = AttendanceStatus.Present;

            MarkAsUpdated();
        }

        public void SetAbsent(string? reason = null)
        {
            if (Status == AttendanceStatus.Absent)
            {
                return;
            }
            Status = AttendanceStatus.Absent;
            CheckInAt = null; 
            CheckOutAt = null; 
            Reason = reason;

            MarkAsUpdated();
        }

        public void SetLate(DateTime? checkInAt = null)
        {
            if (Status == AttendanceStatus.Late)
            {
                return;
            }
            Status = AttendanceStatus.Late;
            CheckInAt = checkInAt;

            MarkAsUpdated();
        }

        public void SetExcuse(string? reason = null)
        {
            if (Status == AttendanceStatus.Excused)
            {
                return;
            }
            Status = AttendanceStatus.Excused;
            CheckInAt = null; 
            CheckOutAt = null; 
            Reason = reason;

            MarkAsUpdated();
        }

        public void SetCheckIn(DateTime checkInAt) 
        { 
            CheckInAt = checkInAt; 

            if (CheckOutAt.HasValue && CheckOutAt.Value < CheckInAt.Value) 
            { 
                throw new ArgumentException("Check-in time cannot be later than check-out time.", nameof(checkInAt)); 
            } 
            MarkAsUpdated(); 
        }

        public void SetCheckOut(DateTime checkOutAt) 
        { 
            if (CheckInAt.HasValue && checkOutAt < CheckInAt.Value) 
            { 
                throw new ArgumentException("Check-out time cannot be earlier than check-in time.", nameof(checkOutAt));
            } 

            CheckOutAt = checkOutAt; 
            MarkAsUpdated(); 
        }

        private static void ValidateTimes(DateTime? checkInAt, DateTime? checkOutAt) 
        { 
            if (checkInAt.HasValue && checkOutAt.HasValue && checkOutAt.Value < checkInAt.Value)
                
            { 
                throw new ArgumentException("Check-out time cannot be earlier than check-in time."); 
            } 
        }
    }
}
