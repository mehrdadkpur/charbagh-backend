using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Domain.Entities.Education.StudentEducation
{
    public sealed class LessonContent : BaseEntity
    {
        public Guid LessonSessionId { get; private set; }

        public Guid BookItemId { get; private set; }

        public LessonContentType Type { get; private set; } = LessonContentType.Teaching;

        public string? FromReference { get; private set; }

        public string? ToReference { get; private set; }

        public string? TeacherNote { get; private set; }

        public LessonSession LessonSession { get; private set; } = null!;

        public BookItem BookItem { get; private set; } = null!;

        private LessonContent() { }

        private LessonContent(
            Guid lessonSessionId,
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? teacherNote
            )
        {
            LessonSessionId = lessonSessionId;
            BookItemId = bookItemId;
            FromReference = fromReference;
            ToReference = toReference;
            TeacherNote = teacherNote;
        }

        public static LessonContent Create(
            Guid lessonSessionId,
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? teacherNote
            )
        {
            if (lessonSessionId == Guid.Empty) 
                throw new ArgumentException("Lesson session is required.", nameof(lessonSessionId));
            
            if (bookItemId == Guid.Empty) 
                throw new ArgumentException("Book item is required.", nameof(bookItemId));

            var lessonContent = new LessonContent(
                lessonSessionId,
                bookItemId,
                fromReference,
                toReference,
                teacherNote);

            lessonContent.MarkAsCreated();
            return lessonContent;
        }

        public void Update(
            Guid bookItemId,
            string? fromReference,
            string? toReference,
            string? teacherNote
            )
        {

            if (bookItemId == Guid.Empty) 
                throw new ArgumentException("Book item is required.", nameof(bookItemId));

            BookItemId =bookItemId;
            FromReference = fromReference;
            ToReference = toReference;
            TeacherNote = teacherNote;

            MarkAsUpdated();
        }

        public void SetTeaching()
        {
            if(Type != LessonContentType.Teaching)
            {
                Type = LessonContentType.Teaching;
            }

            MarkAsUpdated();
        }

        public void SetReview()
        {
            if (Type != LessonContentType.Review)
            {
                Type = LessonContentType.Review;
            }

            MarkAsUpdated();
        }

        public void SetPractice()
        {
            if (Type != LessonContentType.Practice)
            {
                Type = LessonContentType.Practice;
            }

            MarkAsUpdated();
        }

        public void SetPerformance()
        {
            if (Type != LessonContentType.Performance)
            {
                Type = LessonContentType.Performance;
            }

            MarkAsUpdated();
        }
    }
}

