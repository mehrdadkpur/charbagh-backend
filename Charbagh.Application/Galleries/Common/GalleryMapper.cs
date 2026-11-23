using Charbagh.Domain.Entities.CMS;

namespace Charbagh.Application.Galleries.Common
{
    public static class GalleryMapper
    {
        public static GalleryDto ToDto(this Gallery gallery)
        {
            return new GalleryDto(
                gallery.Id, 
                gallery.Title, 
                gallery.Description, 
                gallery.CoverImageUrl, 
                gallery.Status
                );
        }
    }
}
