
namespace Charbagh.Application.InstrumentCategories.Common;

public static class InstrumentCategoryMapper
{
    public static InstrumentCategoryDto ToDto( this InstrumentCategory category)
    {
        return new InstrumentCategoryDto(
            category.Id,
            category.Name,
            category.Description,
            category.IsActive);
    }
}