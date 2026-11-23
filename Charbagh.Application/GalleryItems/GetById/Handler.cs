using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.GetById
{
    public sealed class GetGalleryItemByIdHandler : IRequestHandler<GetGalleryItemByIdQuery, GalleryItemDto>
    {
        private readonly IGalleryItemRepository _repository;

        public GetGalleryItemByIdHandler(IGalleryItemRepository galleryItemRepository)
        {
            _repository = galleryItemRepository;
        }

        public async Task<GalleryItemDto> Handle(GetGalleryItemByIdQuery request, CancellationToken cancellationToken)
        {
            var galleryItem = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (galleryItem == null)
            {
                throw new GalleryItemNotFoundException(request.Id);
            }

            return galleryItem.ToDto();
        }
    }
}
