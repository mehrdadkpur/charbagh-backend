

namespace Charbagh.Application.Instruments.Common
{
    public sealed record InstrumentDto(
        Guid Id, 
        Guid InstrumentCategoryId, 
        string Name, 
        string? EnglishName, 
        string? Description, 
        int DisplayOrder, 
        string? ImageUrl, 
        bool IsActive
        );
}
