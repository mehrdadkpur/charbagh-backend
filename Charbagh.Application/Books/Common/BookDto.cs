namespace Charbagh.Application.Bookitems.Common
{
    public sealed record BookDto(
        Guid Id , 
        Guid InstrumentId , 
        string Title,
        string? EnglishTitle,
        string? Author,
        string? Publisher,
        int? Volume,
        BookLevel Level,
        string? CoverImageUrl,
        int DisplayOrder);
}
