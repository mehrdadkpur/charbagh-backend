using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Galleries.Common;
using MediatR;

namespace Charbagh.Application.Galleries.SetPublish
{
    public sealed class SetPublishGalleryHandler : IRequestHandler<SetPublishGalleryCommand, GalleryDto>
    {
        private readonly IGalleryRepository _repository;
        private readonly IUnitOfWork _unitOfWork;

        public SetPublishGalleryHandler(IGalleryRepository galleryRepository, IUnitOfWork unitOfWork)
        {
            _repository = galleryRepository;
            _unitOfWork = unitOfWork;
        }

        public async Task<GalleryDto> Handle(SetPublishGalleryCommand request , CancellationToken cancellationToken)
        {
            var gallery = await _repository.GetByIdAsync(request.Id, cancellationToken);

            if (gallery == null)
            {
                throw new GalleryNotFoundException(request.Id);
            }

            gallery.Publish();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return gallery.ToDto();
        }
    }
}
