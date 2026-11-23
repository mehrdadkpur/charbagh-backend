
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.Create
{
    public sealed record CreateBookItemCommand(
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
        string? PdfUrl
        ) :IRequest<BookItemDto>;
}
