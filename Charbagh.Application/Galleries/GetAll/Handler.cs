using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetAll
{
    public sealed class GetAllGalleriesHandler : IRequestHandler<GetAllGalleriesQuery, IReadOnlyList<GalleryDto>>
    {
        private readonly IGalleryRepository _repository;

        public GetAllGalleriesHandler(IGalleryRepository galleryRepository)
        {
            _repository = galleryRepository;
        }

        public async Task<IReadOnlyList<GalleryDto>> Handle(GetAllGalleriesQuery request, CancellationToken cancellationToken)
        {
            var galleries = await _repository.GetAllAsync(cancellationToken);

            return galleries.Select(x => x.ToDto()).ToList();
        }
    }
}
