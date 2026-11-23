using Charbagh.Domain.Entities.Education.StudentEducation;
using Charbagh.Domain.Entities.People;
using System.ComponentModel.DataAnnotations;

namespace Charbagh.Domain.Entities.Education.EducationCatalog;

public class Instrument : BaseEntity
{
    [Required]
    public Guid InstrumentCategoryId { get; private set; }

    [Required]
    [MaxLength(100)]
    public string Name { get; private set; } = null!;

    [MaxLength(100)]
    public string? EnglishName { get; private set; }

    [MaxLength(500)]
    public string? Description { get; private set; }

    [MaxLength(300)]
    public string? ImageUrl { get; private set; }

    public int DisplayOrder { get; private set; }

    public bool IsActive { get; private set; } = true;

    #region Navigation Properties

    public InstrumentCategory InstrumentCategory { get; private set; } = null!;

    public ICollection<Book> Books { get; private set; }= new List<Book>();

    public ICollection<TeacherInstrument> TeacherInstruments { get; private set; } = new List<TeacherInstrument>();

    public ICollection<StudentInstrument> StudentInstrument { get; private set; } = new List<StudentInstrument>();

    public ICollection<LessonAssignment> LessonAssignments { get; private set; }  = new List<LessonAssignment>();

    #endregion

    #region Methods
    private Instrument()
    {
    }

    public static Instrument Create( Guid instrumentCategoryId, string name, string? englishName, string? description)
    {
        return new Instrument
        {
            Id = Guid.NewGuid(),
            InstrumentCategoryId = instrumentCategoryId,
            Name = name.Trim(),
            EnglishName = englishName?.Trim(),
            Description = description?.Trim(),
            IsActive = true
        };
    }

    public void Update(Guid instrumentCategoryId, string name, string? englishName, string? description)
    {
        InstrumentCategoryId = instrumentCategoryId;
        Name = name.Trim();
        EnglishName = englishName?.Trim();
        Description = description?.Trim();
    }

    public void Activate()
    {
        IsActive = true;
    }

    public void Deactivate()
    {
        IsActive = false;
    }

    #endregion
}