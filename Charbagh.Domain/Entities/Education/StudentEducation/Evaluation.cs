using Charbagh.Domain.Entities.Identity;
namespace Charbagh.Domain.Entities.Education.StudentEducation
{
    public sealed class Evaluation : BaseEntity
    {
        public Guid LessonSessionId { get; private set; }

        public PracticeCompletionStatus PracticeStatus { get; private set; } = PracticeCompletionStatus.NotAssigned;

        public int? Score { get; private set; }

        public string? Strengths { get; private set; }

        public string? Weaknesses { get; private set; }

        public string? TeacherComment { get; private set; }

        public DateTime EvaluatedAt { get; private set; }

        public Guid EvaluatedByUserId { get; private set; }


        // Navigation Properties

        public LessonSession LessonSession { get; private set; } = null!;

        public User EvaluatedByUser { get; private set; } = null!;

        private Evaluation() { }

        private Evaluation(
            Guid lessonSessionId, 
            PracticeCompletionStatus practiceStatus, 
            int? score, 
            string? strengths, 
            string? weaknesses, 
            string? teacherComment, 
            DateTime evaluatedAt, 
            Guid evaluatedByUserId
            ) 
        { 
            LessonSessionId = lessonSessionId; 
            PracticeStatus = practiceStatus; 
            Score = score; 
            Strengths = strengths; 
            Weaknesses = weaknesses; 
            TeacherComment = teacherComment; 
            EvaluatedAt = evaluatedAt; 
            EvaluatedByUserId = evaluatedByUserId; 
        }

        public static Evaluation Create(
            Guid lessonSessionId, 
            PracticeCompletionStatus practiceStatus, 
            int? score, 
            string? strengths, 
            string? weaknesses, 
            string? teacherComment, 
            DateTime evaluatedAt, 
            Guid evaluatedByUserId
            ) 
        { 
            if (lessonSessionId == Guid.Empty) 
                throw new ArgumentException("Lesson session is required.", nameof(lessonSessionId));

            if (!Enum.IsDefined(typeof(PracticeCompletionStatus), practiceStatus))
                throw new ArgumentOutOfRangeException(nameof(practiceStatus), "Practice completion status is invalid.");
            
            if (evaluatedByUserId == Guid.Empty) 
                throw new ArgumentException("Evaluator user is required.", nameof(evaluatedByUserId));
            
            ValidateScore(score);
            
            if (evaluatedAt.Kind != DateTimeKind.Utc) 
                throw new ArgumentException("Evaluation time must be UTC.", nameof(evaluatedAt));
            
            var evaluation = new Evaluation(
                lessonSessionId, 
                practiceStatus, 
                score, 
                strengths, 
                weaknesses, 
                teacherComment, 
                evaluatedAt, 
                evaluatedByUserId
                ); 
            evaluation.MarkAsCreated();
            
            return evaluation; 
        }

        public void Update(
            PracticeCompletionStatus practiceStatus, 
            int? score, 
            string? strengths, 
            string? weaknesses, 
            string? teacherComment
            ) 
        { 
            if (!Enum.IsDefined(typeof(PracticeCompletionStatus), practiceStatus))
                throw new ArgumentOutOfRangeException(nameof(practiceStatus), "Practice completion status is invalid.");
            ValidateScore(score); 
            PracticeStatus = practiceStatus; 
            Score = score; 
            Strengths = strengths; 
            Weaknesses = weaknesses; 
            TeacherComment = teacherComment; 

            MarkAsUpdated(); 
        }

        public void SetNotAssignedPracticeCompletion()
        {
            PracticeStatus = PracticeCompletionStatus.NotAssigned;

            MarkAsUpdated();
        }

        public void SetNotCompletedPracticeCompletion()
        {
            PracticeStatus = PracticeCompletionStatus.NotCompleted;

            MarkAsUpdated();
        }

        public void SetPartiallyCompletedPracticeCompletion()
        {
            PracticeStatus = PracticeCompletionStatus.PartiallyCompleted;

            MarkAsUpdated();
        }

        public void SetCompletePracticeCompletion()
        {
            PracticeStatus = PracticeCompletionStatus.Completed;

            MarkAsUpdated();
        }

        private static void ValidateScore(int? score) 
        { 
            if (score is < 0 or > 100) 
                throw new ArgumentOutOfRangeException(nameof(score), "Score must be between 0 and 100.");
        }
    }
}
