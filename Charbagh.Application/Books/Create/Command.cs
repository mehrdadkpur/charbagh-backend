
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.Create
{
    public sealed record CreateBookCommand(
        Guid InstrumentId,
        string Title,
        string? EnglishTitle,
        string? Author,
        string? Publisher,
        int? Volume,
        BookLevel Level,
        string? CoverImageUrl,
        int DisplayOrder
        ) : IRequest<BookDto>;
}
