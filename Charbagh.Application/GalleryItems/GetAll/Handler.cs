using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.GalleryItems.Common;
using MediatR;

namespace Charbagh.Application.GalleryItems.GetAll
{
    public sealed class GetAllGalleryItemsHandler : IRequestHandler<GetAllGalleryItemsQuery, IReadOnlyList<GalleryItemDto>>
    {
        private readonly IGalleryItemRepository _repository;

        public GetAllGalleryItemsHandler(IGalleryItemRepository galleryItemRepository)
        {
            _repository = galleryItemRepository;
        }

        public async Task<IReadOnlyList<GalleryItemDto>> Handle(GetAllGalleryItemsQuery request, CancellationToken cancellationToken)
        {
            var galleryItems = await _repository.GetAllAsync(cancellationToken);

            return galleryItems.Select(x => x.ToDto()).ToList();
        }
    }
}
