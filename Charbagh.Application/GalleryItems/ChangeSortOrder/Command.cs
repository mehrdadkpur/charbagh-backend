
using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.ChangeSortOrder
{
    public sealed record ChangeSortOrderGalleryItemCommand(Guid Id , int SortOrder):IRequest<GalleryItemDto>;
}
