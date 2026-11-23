using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.GetById
{
    public sealed record GetGalleryItemByIdQuery(Guid Id):IRequest<GalleryItemDto>;
}
