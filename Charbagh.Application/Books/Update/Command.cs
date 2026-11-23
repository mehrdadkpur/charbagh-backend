
using Charbagh.Application.Bookitems.Common;
using MediatR;

namespace Charbagh.Application.Bookitems.Update
{
    public sealed record UpdateBookCommand(
        Guid Id,
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
