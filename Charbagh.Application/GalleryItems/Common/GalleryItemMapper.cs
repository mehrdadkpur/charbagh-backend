using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.GalleryItems.Common
{
    public static class GalleryItemMapper
    {
        public static GalleryItemDto ToDto(this GalleryItem item)
        {
            return new GalleryItemDto(
                item.Id,
                item.GalleryId,
                item.MediaType,
                item.Title,
                item.Description,
                item.FileUrl,
                item.ThumbnailUrl,
                item.SortOrder);
        }
    }
}
