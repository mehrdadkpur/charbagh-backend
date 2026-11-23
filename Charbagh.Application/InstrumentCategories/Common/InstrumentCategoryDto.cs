

namespace Charbagh.Application.InstrumentCategories.Common
{
    public sealed record InstrumentCategoryDto(Guid Id, string Name, string? Description, bool IsActive);
}
