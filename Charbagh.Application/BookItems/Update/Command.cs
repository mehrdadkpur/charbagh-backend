
using Charbagh.Application.BookItems.Common;
using MediatR;

namespace Charbagh.Application.BookItems.Update
{
    public sealed record UpdateBookItemCommand(
        Guid Id, 
        Guid BookId ,
        string Title,
        string? Description,
        int DisplayOrder,
        string? Number,
        BookItemType Type,
        BookReferenceType ReferenceType,
        int? RecommendedPracticeDays,
        int DifficultyLevel,
        string? AudioUrl,
        string? VideoUrl,
        string? PdfUrl
        ) : IRequest<BookItemDto>;
}
