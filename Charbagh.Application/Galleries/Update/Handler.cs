using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.Update
{
    public sealed class UpdateGalleryHandler : IRequestHandler<UpdateGalleryCommand, GalleryDto>
    {
        private readonly IGalleryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateGalleryHandler(IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
            _repository = galleryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryDto> Handle(UpdateGalleryCommand request , CancellationToken cancellationToken)
        {
            var gallery = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (gallery == null)
            {
                throw new GalleryNotFoundException(request.Id);
            }

            gallery.Update(request.Title, request.Description , request.CoverImageUrl);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return gallery.ToDto();
        }
    }
}
