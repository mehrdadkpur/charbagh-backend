using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Deactivate
{
    public sealed class DeactivateTagHandler : IRequestHandler<DeactivateTagCommand , TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public DeactivateTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<TagDto> Handle(DeactivateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.Id , cancellationToken);

            if (tag == null)
            {
                throw new TagNotFoundException(request.Id);
            }
            
            tag.Deactivate();

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag.ToDto();
        }
    }
}
