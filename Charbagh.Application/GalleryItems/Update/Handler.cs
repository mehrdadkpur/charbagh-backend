using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.Update
{
    public sealed class UpdateGalleryItemHandler : IRequestHandler<UpdateGalleryItemCommand, GalleryItemDto>
    {
        private readonly IGalleryItemRepository _galleryItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGalleryItemHandler(IGalleryItemRepository galleryItemRepository, IUnitOfWork unitOfWork)
        {
            _galleryItemRepository = galleryItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryItemDto> Handle(UpdateGalleryItemCommand reqest, CancellationToken cancellationToken)
        {
            var galleryItem = await _galleryItemRepository.GetByIdAsync(reqest.Id, cancellationToken);

            if (galleryItem == null)
            {
                throw new GalleryItemNotFoundException(reqest.Id);
            }

            galleryItem.Update(reqest.Title, reqest.Description, reqest.ThumbnailUrl, reqest.FileUrl);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return galleryItem.ToDto();
        }
    }
}
