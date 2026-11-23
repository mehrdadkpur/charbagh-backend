using Charbagh.Domain.Enums;

namespace Charbagh.Application.GalleryItems.Common
{
    public sealed record GalleryItemDto(
        Guid Id,
        Guid GalleryId,
        GalleryMediaType MediaType,
        string? Title,
        string? Description,
        string FileUrl,
        string? ThumbnailUrl,
        int SortOrder
        );
}
