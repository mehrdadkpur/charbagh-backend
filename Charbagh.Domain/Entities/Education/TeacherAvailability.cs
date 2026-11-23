namespace Charbagh.Domain.Entities.Education
{
    public class TeacherAvailability
    {
        public Guid Id { get; set; }

        public Guid TeacherId { get; set; }

        public DayOfWeek Day { get; set; }

        public TimeSpan StartTime { get; set; }

        public TimeSpan EndTime { get; set; }
    }
}
