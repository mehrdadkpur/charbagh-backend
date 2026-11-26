using Charbagh.Domain.Entities.Education.EducationCatalog;
namespace Charbagh.Domain.Entities.Education.StudentEducation
{
    public sealed class PracticeAssignment : BaseEntity
    {
        public Guid LessonSessionId { get; private set; }

        public Guid BookItemId { get; private set; }

        public string? FromReference { get; private set; }

        public string? ToReference { get; private set; }

        public string? Instruction { get; private set; }

        public DateOnly? DueDate { get; private set; }

        public PracticePriority Priority { get; private set; }

        // Navigation Properties

        public LessonSession LessonSession { get; private set; } = null!;

        public BookItem BookItem { get; private set; } = null!;

        private PracticeAssignment() { }

        private PracticeAssignment(
            Guid lessonSessionId,
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? instruction,
            DateOnly? dueDate,
            PracticePriority priority
            )
        {
            LessonSessionId = lessonSessionId;
            BookItemId = bookItemId;
            FromReference = fromReference;
            ToReference = toReference;
            Instruction = instruction;
            DueDate = dueDate;
            Priority = priority;

        }

        public static PracticeAssignment Create(
            Guid lessonSessionId,
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? instruction,
            DateOnly? dueDate,
            PracticePriority priority
            )
        {
            if (lessonSessionId == Guid.Empty)
                throw new ArgumentException("Lesson session is required.", nameof(lessonSessionId));


            if (bookItemId == Guid.Empty)
                throw new ArgumentException("Book Item is required.", nameof(bookItemId));

            if (!Enum.IsDefined(typeof(PracticePriority), priority))
                throw new ArgumentOutOfRangeException(nameof(priority), "Practice priority is invalid.");

            var practiceAssignment = new PracticeAssignment(
                lessonSessionId,
                bookItemId, 
                fromReference, 
                toReference, 
                instruction,
                dueDate,
                priority

                );
            practiceAssignment.MarkAsCreated();

            return practiceAssignment;
        }

        public void Update(
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? instruction,
            DateOnly? dueDate
            )
        {
            if (bookItemId == Guid.Empty)
                throw new ArgumentException("Book Item is required.", nameof(bookItemId));

            BookItemId = bookItemId;
            FromReference = fromReference;
            ToReference = toReference;
            Instruction = instruction;
            DueDate = dueDate;

            MarkAsUpdated();
        }

        public void SetNormalPracticePriority()
        {
            if (Priority == PracticePriority.Normal)
            {
                return;
            }
            Priority = PracticePriority.Normal;

            MarkAsUpdated();
        }

        public void SetLowPracticePriority()
        {
            if (Priority == PracticePriority.Low)
            {
                return;
            }
            Priority = PracticePriority.Low;

            MarkAsUpdated();
        }

        public void SetHighPracticePriority()
        {
            if (Priority == PracticePriority.High)
            {
                return;
            }
            Priority = PracticePriority.High;

            MarkAsUpdated();
        }
    }
}
