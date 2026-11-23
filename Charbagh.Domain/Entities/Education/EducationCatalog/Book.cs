using Charbagh.Domain.Entities.Education.StudentEducation;

namespace Charbagh.Domain.Entities.Education.EducationCatalog;

public class Book : BaseEntity
{
    public Guid InstrumentId { get; private set; }

    public string Title { get; private set; } = null!;

    public string? EnglishTitle { get; private set; }

    public string? Author { get; private set; }

    public string? Publisher { get; private set; }

    public int? Volume { get; private set; }

    public BookLevel Level { get; private set; }

    public string? CoverImageUrl { get; private set; }

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; } = true;


    #region Navigation Properties

    public Instrument Instrument { get; private set; } = null!;

    public ICollection<BookItem> BookItems { get; private set; }= new List<BookItem>();

    public ICollection<LessonAssignment> LessonAssignments { get; private set; } = new List<LessonAssignment>();

    public ICollection<TeachingPlan> TeachingPlans { get; private set; }= new List<TeachingPlan>();

    #endregion


    private Book()
    {
    }


    private Book(
        Guid instrumentId,
        string title,
        string? englishTitle,
        string? author,
        string? publisher,
        int? volume,
        BookLevel level,
        string? coverImageUrl,
        int displayOrder)
    {
        InstrumentId = instrumentId;

        Title = title.Trim();

        EnglishTitle = string.IsNullOrWhiteSpace(englishTitle)
            ? null
            : englishTitle.Trim();

        Author = string.IsNullOrWhiteSpace(author)
            ? null
            : author.Trim();

        Publisher = string.IsNullOrWhiteSpace(publisher)
            ? null
            : publisher.Trim();

        Volume = volume;

        Level = level;

        CoverImageUrl = coverImageUrl;

        DisplayOrder = displayOrder;

        IsActive = true;
    }


    #region Factory

    public static Book Create(
        Guid instrumentId,
        string title,
        string? englishTitle,
        string? author,
        string? publisher,
        int? volume,
        BookLevel level,
        string? coverImageUrl,
        int displayOrder)
    {
        if (instrumentId == Guid.Empty)
            throw new ArgumentException(
                "Instrument is required.",
                nameof(instrumentId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Book title is required.",
                nameof(title));

        if (volume is not null && volume <= 0)
            throw new ArgumentException(
                "Volume must be greater than zero.",
                nameof(volume));

        if (displayOrder < 0)
            throw new ArgumentException(
                "Display order cannot be negative.",
                nameof(displayOrder));


        var book = new Book(
            instrumentId,
            title,
            englishTitle,
            author,
            publisher,
            volume,
            level,
            coverImageUrl,
            displayOrder);

        book.MarkAsCreated();

        return book;
    }

    #endregion


    #region Update

    public void Update(
        Guid instrumentId,
        string title,
        string? englishTitle,
        string? author,
        string? publisher,
        int? volume,
        BookLevel level,
        string? coverImageUrl,
        int displayOrder)
    {
        if (instrumentId == Guid.Empty)
            throw new ArgumentException(
                "Instrument is required.",
                nameof(instrumentId));

        if (string.IsNullOrWhiteSpace(title))
            throw new ArgumentException(
                "Book title is required.",
                nameof(title));

        if (volume is not null && volume <= 0)
            throw new ArgumentException(
                "Volume must be greater than zero.",
                nameof(volume));

        if (displayOrder < 0)
            throw new ArgumentException(
                "Display order cannot be negative.",
                nameof(displayOrder));


        InstrumentId = instrumentId;

        Title = title.Trim();

        EnglishTitle = string.IsNullOrWhiteSpace(englishTitle)
            ? null
            : englishTitle.Trim();

        Author = string.IsNullOrWhiteSpace(author)
            ? null
            : author.Trim();

        Publisher = string.IsNullOrWhiteSpace(publisher)
            ? null
            : publisher.Trim();

        Volume = volume;

        Level = level;

        CoverImageUrl = coverImageUrl;

        DisplayOrder = displayOrder;

        MarkAsUpdated();
    }

    #endregion


    #region Status

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

    #endregion
}