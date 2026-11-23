
using Charbagh.Application.GalleryItems.Common;
using Charbagh.Domain.Enums;
using MediatR;

namespace Charbagh.Application.GalleryItems.Create
{
    public sealed record CreateGalleryItemCommand(
        Guid GalleryId,
        GalleryMediaType MediaType,
        string? Title,
        string? Description,
        string FileUrl,
        string? ThumbnailUrl,
        int SortOrder
        ) :IRequest<GalleryItemDto>;
}
