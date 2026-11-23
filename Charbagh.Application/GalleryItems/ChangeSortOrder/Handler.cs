using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.ChangeSortOrder
{
    public sealed class ChangeSortOrderGalleryItemHandler : IRequestHandler<ChangeSortOrderGalleryItemCommand, GalleryItemDto>
    {
        private readonly IGalleryItemRepository _galleryItemRepository;
        private readonly IUnitOfWork _unitOfWork;

        public ChangeSortOrderGalleryItemHandler(IGalleryItemRepository galleryItemRepository,IUnitOfWork unitOfWork)
        {
            _galleryItemRepository = galleryItemRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryItemDto> Handle(ChangeSortOrderGalleryItemCommand reqest, CancellationToken cancellationToken)
        {
            var galleryItem = await _galleryItemRepository.GetByIdAsync(reqest.Id, cancellationToken);

            if (galleryItem == null)
            {
                throw new GalleryItemNotFoundException(reqest.Id);
            }

            galleryItem.ChangeSortOrder(reqest.SortOrder);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return galleryItem.ToDto();
        }
    }
}
