
namespace Charbagh.Application.Galleries.Common
{
    public sealed record GalleryDto(
        Guid Id,
        string Title,
        string? Description,
        string? CoverImageUrl,
        GalleryStatus Status
        );
}
