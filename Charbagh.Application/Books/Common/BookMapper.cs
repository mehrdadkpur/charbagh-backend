using Charbagh.Domain.Entities.Education.EducationCatalog;

namespace Charbagh.Application.Bookitems.Common
{
    public static class BookMapper
    {
        public static BookDto ToDto(this Book book)
        {
            return new BookDto(
                book.Id,
                book.InstrumentId,
                book.Title,
                book.EnglishTitle,
                book.Author,
                book.Publisher,
                book.Volume,
                book.Level,
                book.CoverImageUrl,
                book.DisplayOrder
                );
        }
    }
}
