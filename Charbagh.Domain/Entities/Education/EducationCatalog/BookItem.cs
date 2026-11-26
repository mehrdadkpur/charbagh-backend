using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Domain.Entities.Education.EducationCatalog
{
    public sealed class BookItem : BaseEntity
    {
        public Guid BookId { get; private set; }

        public int DisplayOrder { get; private set; }

        public string? Number { get; private set; }

        public string Title { get; private set; } = null!;

        public BookItemType Type { get; private set; }

        public BookReferenceType ReferenceType { get; private set; }

        public string? Description { get; private set; }

        public int? RecommendedPracticeDays { get; private set; }

        public int DifficultyLevel { get; private set; } = 1;

        public string? AudioUrl { get; private set; }

        public string? VideoUrl { get; private set; }

        public string? PdfUrl { get; private set; }

        public bool IsActive { get; private set; } = true;

        // Navigation
        public Book Book { get; private set; } = null!;

        public ICollection<PracticeAssignment> PracticeAssignments { get; private set; }
            = new List<PracticeAssignment>();

        public ICollection<StudentProgress> StudentProgresses { get; private set; }
            = new List<StudentProgress>();

        public ICollection<LessonSession> LessonSessions { get; private set; }
            = new List<LessonSession>();


        private BookItem()
        {
        }


        private BookItem(
            Guid bookId,
            int displayOrder,
            string? number,
            string title,
            BookItemType type,
            BookReferenceType referenceType,
            string? description,
            int? recommendedPracticeDays,
            int difficultyLevel,
            string? audioUrl,
            string? videoUrl,
            string? pdfUrl)
        {
            BookId = bookId;

            DisplayOrder = displayOrder;

            Number = number?.Trim();

            Title = title.Trim();

            Type = type;

            ReferenceType = referenceType;

            Description = description?.Trim();

            RecommendedPracticeDays = recommendedPracticeDays;

            DifficultyLevel = difficultyLevel;

            AudioUrl = audioUrl?.Trim();

            VideoUrl = videoUrl?.Trim();

            PdfUrl = pdfUrl?.Trim();

            IsActive = true;
        }


        public static BookItem Create(
            Guid bookId,
            int displayOrder,
            string? number,
            string title,
            BookItemType type,
            BookReferenceType referenceType,
            string? description,
            int? recommendedPracticeDays,
            int difficultyLevel,
            string? audioUrl,
            string? videoUrl,
            string? pdfUrl)
        {
            if (bookId == Guid.Empty)
                throw new ArgumentException(
                    "Book is required.",
                    nameof(bookId));

            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(
                    "Book item title is required.",
                    nameof(title));

            if (displayOrder <= 0)
                throw new ArgumentException(
                    "Display order must be greater than zero.",
                    nameof(displayOrder));

            if (difficultyLevel < 1)
                throw new ArgumentException(
                    "Difficulty level must be greater than zero.",
                    nameof(difficultyLevel));

            if (recommendedPracticeDays is <= 0)
                throw new ArgumentException(
                    "Recommended practice days must be greater than zero.");

            if (!Enum.IsDefined(typeof(BookItemType), type))
                throw new ArgumentOutOfRangeException(nameof(type), "Book item type is invalid.");

            if (!Enum.IsDefined(typeof(BookReferenceType), referenceType))
                throw new ArgumentOutOfRangeException(nameof(referenceType), "Book reference type is invalid.");

            var bookItem = new BookItem(
                bookId,
                displayOrder,
                number,
                title,
                type,
                referenceType,
                description,
                recommendedPracticeDays,
                difficultyLevel,
                audioUrl,
                videoUrl,
                pdfUrl);

            bookItem.MarkAsCreated();

            return bookItem;
        }


        public void Update(
            string title,
            string? description,
            int displayOrder,
            string? number,
            BookItemType type,
            BookReferenceType referenceType,
            int? recommendedPracticeDays,
            int difficultyLevel,
            string? audioUrl,
            string? videoUrl,
            string? pdfUrl)
        {
            if (string.IsNullOrWhiteSpace(title))
                throw new ArgumentException(
                    "Book item title is required.",
                    nameof(title));

            if (displayOrder <= 0)
                throw new ArgumentException(
                    "Display order must be greater than zero.",
                    nameof(displayOrder));

            if (difficultyLevel < 1)
                throw new ArgumentException(
                    "Difficulty level must be greater than zero.",
                    nameof(difficultyLevel));

            if (recommendedPracticeDays is <= 0)
                throw new ArgumentException("Recommended practice days must be greater than zero.", nameof(recommendedPracticeDays));

            if (!Enum.IsDefined(typeof(BookItemType), type))
                throw new ArgumentOutOfRangeException(nameof(type), "Book item type is invalid.");

            if (!Enum.IsDefined(typeof(BookReferenceType), referenceType))
                throw new ArgumentOutOfRangeException(nameof(referenceType), "Book reference type is invalid.");

            DisplayOrder = displayOrder;

            Number = number?.Trim();

            Title = title.Trim();

            Type = type;

            ReferenceType = referenceType;

            Description = description?.Trim();

            RecommendedPracticeDays = recommendedPracticeDays;

            DifficultyLevel = difficultyLevel;

            AudioUrl = audioUrl?.Trim();

            VideoUrl = videoUrl?.Trim();

            PdfUrl = pdfUrl?.Trim();

            MarkAsUpdated();
        }


        public void Activate()
        {
            if (IsActive)
                return;

            IsActive = true;

            MarkAsUpdated();
        }


        public void Deactivate()
        {
            if (!IsActive)
                return;

            IsActive = false;

            MarkAsUpdated();
        }
    }
}