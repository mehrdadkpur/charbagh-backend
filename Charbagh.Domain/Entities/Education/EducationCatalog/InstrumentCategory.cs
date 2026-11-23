using Charbagh.Domain.Entities.Education.EducationCatalog;

public class InstrumentCategory : BaseEntity
{
    public string Name { get; private set; } = null!;

    public string? Description { get; private set; }

    public bool IsActive { get; private set; }

    public ICollection<Instrument> Instruments { get; private set; } = new List<Instrument>();

    private InstrumentCategory()
    {
    }

    public static InstrumentCategory Create( string name, string? description)
    {
        return new InstrumentCategory
        {
            Id = Guid.NewGuid(),
            Name = name.Trim(),
            Description = description?.Trim(),
            IsActive = true
        };
    }

    public void Update( string name, string? description)
    {
        Name = name.Trim();
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
}