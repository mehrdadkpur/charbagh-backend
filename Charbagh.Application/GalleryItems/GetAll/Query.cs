using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.GetAll
{
    public sealed record GetAllGalleryItemsQuery():IRequest<IReadOnlyList< GalleryItemDto>>;
}
