using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.BookItems.Common
{
    public static class BookItemMapper
    {
        public static BookItemDto ToDto(this BookItem bookItem)
        {
            return new BookItemDto(
                bookItem.Id,
                bookItem.BookId,
                bookItem.Title,
                bookItem.Description,
                bookItem.Number,
                bookItem.Type,
                bookItem.ReferenceType,
                bookItem.RecommendedPracticeDays,
                bookItem.DisplayOrder,
                bookItem.DifficultyLevel,
                bookItem.AudioUrl,
                bookItem.VideoUrl,
                bookItem.PdfUrl
                );
        }
    }
}
