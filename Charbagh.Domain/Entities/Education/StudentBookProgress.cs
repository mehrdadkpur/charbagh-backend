namespace Charbagh.Domain.Entities.Education
{
    public class StudentBookProgress
    {
        public Guid Id { get; set; }

        public Guid LessonAssignmentId { get; set; }

        public Guid BookContentId { get; set; }

        public StudentBookProgressStatus Status { get; set; }

        public DateTime? StartedAt { get; set; }

        public DateTime CompletedAt { get; set; }

        public string TeacherNote { get; set; }

    }

}
