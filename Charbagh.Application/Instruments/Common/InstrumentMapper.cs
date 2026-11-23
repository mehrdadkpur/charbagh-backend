using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Instruments.Common;

public static class InstrumentMapper
{
    public static InstrumentDto ToDto(this Instrument instrument)
    {
        return new InstrumentDto(
            instrument.Id,
            instrument.InstrumentCategoryId,
            instrument.Name,
            instrument.EnglishName,
            instrument.Description,
            instrument.DisplayOrder,
            instrument.ImageUrl,
            instrument.IsActive);
    }
}