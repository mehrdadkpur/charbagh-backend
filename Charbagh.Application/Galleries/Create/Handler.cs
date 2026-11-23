using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Galleries.Common;
using Charbagh.Domain.Entities.CMS;
using MediatR;

namespace Charbagh.Application.Galleries.Create
{
    public sealed class CreateGalleryHandler : IRequestHandler<CreateGalleryCommand , GalleryDto>
    {
        private readonly IGalleryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public CreateGalleryHandler(IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
            _repository = galleryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryDto> Handle(CreateGalleryCommand request , CancellationToken cancellationToken)
        {
            var gallery = Gallery.Create(request.Title, request.Description, request.CoverImageUrl);

            await _repository.AddAsync(gallery , cancellationToken);
            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return gallery.ToDto();
        }
    }
}
