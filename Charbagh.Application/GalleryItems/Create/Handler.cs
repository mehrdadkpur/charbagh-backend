using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.GalleryItems.Common;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.GalleryItems.Create
{
    public sealed class CreateGalleryItemHandler : IRequestHandler<CreateGalleryItemCommand , GalleryItemDto>
    {
        private readonly IGalleryItemRepository _galleryItemRepository;
        private readonly IGalleryRepository _galleryRepository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGalleryItemHandler(IGalleryItemRepository galleryItemRepository , IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
             _galleryRepository = galleryRepository;
            _galleryItemRepository = galleryItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryItemDto> Handle(CreateGalleryItemCommand reqest , CancellationToken cancellationToken)
        {
            var gallery = await _galleryRepository.GetByIdAsync(reqest.GalleryId , cancellationToken);

            if (gallery == null)
            {
                throw new GalleryNotFoundException(reqest.GalleryId);
            }

            var galleryItem = GalleryItem.Create(
                reqest.GalleryId , 
                reqest.MediaType , 
                reqest.FileUrl , 
                reqest.Title , 
                reqest.Description , 
                reqest.ThumbnailUrl ,
                reqest.SortOrder);

            await _galleryItemRepository.AddAsync(galleryItem, cancellationToken);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return galleryItem.ToDto();
        }

    }
}
