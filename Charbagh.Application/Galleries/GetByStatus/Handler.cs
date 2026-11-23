using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetByStatus
{
    public sealed class GetByStatusGalleriesHandler : IRequestHandler<GetByStatusGalleriesQuery, IReadOnlyList<GalleryDto>>
    {
        private readonly IGalleryRepository _repository;

        public GetByStatusGalleriesHandler(IGalleryRepository galleryRepository)
        {
            _repository = galleryRepository;
        }

        public async Task<IReadOnlyList<GalleryDto>> Handle(GetByStatusGalleriesQuery request, CancellationToken cancellationToken)
        {
            var galleries = await _repository.GetByGalleryStatusAsync(request.Status,cancellationToken);

            return galleries.Select(x => x.ToDto()).ToList();
        }
    }
}
