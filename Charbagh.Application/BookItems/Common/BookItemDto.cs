
namespace Charbagh.Application.BookItems.Common
{
    public sealed record BookItemDto (
        Guid Id,
        Guid BookId,
        string Title,
        string? Description,
        string? Number,
        BookItemType Type,
        BookReferenceType ReferenceType,
        int? RecommendedPracticeDays,
        int DisplayOrder,
        int DifficultyLevel,
        string? AudioUrl,
        string? VideoUrl,
        string? PdfUrl,
        bool IsActive
        );
}
