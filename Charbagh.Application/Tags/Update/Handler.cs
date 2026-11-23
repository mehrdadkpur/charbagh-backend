using Charbagh.Application.Abstractions.Persistence;
using Charbagh.Application.Common.Exceptions;
using Charbagh.Application.Tags.Common;
using MediatR;

namespace Charbagh.Application.Tags.Update
{
    public sealed class UpdateTagHandler : IRequestHandler<UpdateTagCommand , TagDto>
    {
        private readonly ITagRepository _tagRepository;
        private readonly IUnitOfWork _unitOfWork;

        public UpdateTagHandler(ITagRepository tagRepository, IUnitOfWork unitOfWork)
        {
            _tagRepository = tagRepository;
            _unitOfWork = unitOfWork;
        }
        public async Task<TagDto> Handle(UpdateTagCommand request, CancellationToken cancellationToken)
        {
            var tag = await _tagRepository.GetByIdAsync(request.Id , cancellationToken);

            if (tag == null)
            {
                throw new TagNotFoundException(request.Id);
            }

            var existsName = await _tagRepository.ExistsTagNameAsync(request.Name , cancellationToken);

            if (existsName)
            {
                throw new DuplicateTagNameException(request.Name);
            }
            
            tag.Update(request.Name);

            await _unitOfWork.SaveChangesAsync(cancellationToken);

            return tag.ToDto();
        }
    }
}
