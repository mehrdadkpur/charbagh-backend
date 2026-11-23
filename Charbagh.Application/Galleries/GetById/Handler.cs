
using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.GetById
{
    public sealed class GetGalleryByIdHandler : IRequestHandler<GetGalleryByIdQuery, GalleryDto>
    {
        private readonly IGalleryRepository _repository;

        public GetGalleryByIdHandler(IGalleryRepository galleryRepository)
        {
            _repository = galleryRepository;
        }

        public async Task<GalleryDto> Handle(GetGalleryByIdQuery request, CancellationToken cancellationToken)
        {
            var gallery = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (gallery == null)
            {
                throw new GalleryNotFoundException(request.Id);
            }

            return gallery.ToDto();
        }
    }
}
