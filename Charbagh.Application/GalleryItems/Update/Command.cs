using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.Update
{
    public sealed record UpdateGalleryItemCommand(
        Guid Id , 
        string? Title,
        string? Description,
        string? ThumbnailUrl,
        string FileUrl) : IRequest<GalleryItemDto>;
}
