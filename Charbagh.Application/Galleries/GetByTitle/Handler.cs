using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetByTitle
{
    public sealed class GetByTitleGalleriesHandler : IRequestHandler<GetByTitleGalleriesQuery, IReadOnlyList<GalleryDto>>
    {
        private readonly IGalleryRepository _repository;

        public GetByTitleGalleriesHandler(IGalleryRepository galleryRepository)
        {
            _repository = galleryRepository;
        }

        public async Task<IReadOnlyList<GalleryDto>> Handle(GetByTitleGalleriesQuery request, CancellationToken cancellationToken)
        {
            var galleries = await _repository.GetByGalleryTitleAsync(request.Title,cancellationToken);

            return galleries.Select(x => x.ToDto()).ToList();
        }
    }
}
