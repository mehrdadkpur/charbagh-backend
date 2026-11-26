using Charbagh.Domain.Common;

namespace Charbagh.Domain.Entities.Education.StudentEducation
{
    public sealed class LessonSession : BaseEntity
    {
        public Guid LessonAssignmentId { get; private set; }

        public DateTime SessionDate { get; private set; }

        public TimeSpan? StartTime { get; private set; }

        public TimeSpan? EndTime { get; private set; }

        public int? DurationMinutes { get; private set; }

        public LessonSessionStatus Status { get; private set; } = LessonSessionStatus.Scheduled;

        public string? TeacherNote { get; private set; }

        // Navigation

        public LessonAssignment LessonAssignment { get; private set; } = null!;

        public Attendance? Attendance { get; private set; }

        public Evaluation? Evaluation { get; private set; }

        public ICollection<LessonContent> LessonContents { get; private set; } = new List<LessonContent>();

        public ICollection<PracticeAssignment> PracticeAssignments { get; private set; } = new List<PracticeAssignment>();

        private LessonSession() { }

        private LessonSession(
            Guid lessonAssignmentId,
            DateTime sessionDate,
            TimeSpan? startTime,
            TimeSpan? endTime,
            int? durationMinutes,
            string? teacherNote
            )
        {
            LessonAssignmentId = lessonAssignmentId;
            SessionDate = sessionDate;
            StartTime = startTime;
            EndTime = endTime;
            DurationMinutes = durationMinutes;
            TeacherNote = teacherNote;
        }

        public static LessonSession Create(
            Guid lessonAssignmentId,
            DateTime sessionDate,
            TimeSpan? startTime,
            TimeSpan? endTime,
            int? durationMinutes,
            string? teacherNote
            )
        {

            if (lessonAssignmentId == Guid.Empty) 
                throw new ArgumentException("Lesson assignment is required.", nameof(lessonAssignmentId)); 

            ValidateTimes(startTime, endTime); 
            if (durationMinutes.HasValue && durationMinutes.Value <= 0) 
                throw new ArgumentException("Duration must be greater than zero.", nameof(durationMinutes));

            var lessonSession = new LessonSession(
                lessonAssignmentId,
                sessionDate, 
                startTime, 
                endTime, 
                durationMinutes, 
                teacherNote);
            lessonSession.MarkAsCreated();
            return lessonSession;
        }

        public void Update(
            DateTime sessionDate,
            TimeSpan? startTime,
            TimeSpan? endTime,
            int? durationMinutes,
            string? teacherNote)
        {

            if (Status != LessonSessionStatus.Scheduled) 
            {
                
                throw new InvalidOperationException("Only scheduled lesson sessions can be updated."); 
            }

            ValidateTimes(startTime, endTime); 

            if (durationMinutes.HasValue && durationMinutes.Value <= 0) 
                throw new ArgumentException("Duration must be greater than zero.", nameof(durationMinutes));

            SessionDate = sessionDate;
            StartTime = startTime;
            EndTime = endTime;
            DurationMinutes = durationMinutes;
            TeacherNote = teacherNote;

            MarkAsUpdated();
        }

        public void Complete()
        {
            if (Status != LessonSessionStatus.Scheduled)
            {
                throw new InvalidOperationException("Only Scheduled lesson Sessions can be Complete.");
            }

            Status = LessonSessionStatus.Completed;

            MarkAsUpdated();
        }

        public void Cancel()
        {
            if (Status != LessonSessionStatus.Scheduled)
            {
                throw new InvalidOperationException("Only Scheduled lesson Sessions can be Cancel.");
            }

            Status = LessonSessionStatus.Cancelled;

            MarkAsUpdated();
        }

        public void StudentAbsent()
        {
            if (Status != LessonSessionStatus.Scheduled)
            {
                throw new InvalidOperationException("Only Scheduled lesson Sessions can be Student Absent.");
            }

            Status = LessonSessionStatus.StudentAbsent;

            MarkAsUpdated();
        }

        public void TeacherAbsent()
        {
            if (Status != LessonSessionStatus.Scheduled)
            {
                throw new InvalidOperationException("Only Scheduled lesson Sessions can be Teacher Absent.");
            }

            Status = LessonSessionStatus.TeacherAbsent;

            MarkAsUpdated();
        }

        private static void ValidateTimes(TimeSpan? startTime, TimeSpan? endTime) 
        
        { 
            if (startTime.HasValue && endTime.HasValue && endTime.Value < startTime.Value) 
                
            { 
                    throw new ArgumentException("End time cannot be earlier than start time."); 
            } 
        }
    }

}
